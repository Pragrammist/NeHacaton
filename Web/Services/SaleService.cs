using Web.Dtos.Sales.Inventory;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using AutoMapper;
using DataBase;
using Web.Cryptography;
using Web.Search.Inventory;
using HendInRentApi.Dto.Inventory;
using DataBase.Entities;
using Web.Geolocation;
using Web.Caching;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Web.Services
{
    public class SaleService 
    {
        readonly HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> _inventoryRepo;
        readonly IMapper _mapper;
        readonly UserContext _userContext;
        readonly InventoryTagSearcher _searcher;
        readonly ApiTokenProvider _apiTokenProvider;
        readonly GeolocationRepository _geolocationRepo;
        Cacher<User, string> _userCache;
        Cacher<OutputInventoryDto, string> _inventoryCacher;
        public SaleService(
            HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> inventoryRepo,
            IMapper mapper, 
            UserContext userContext, 
            InventoryTagSearcher searcher,
            ApiTokenProvider apiTokenProvider,
            GeolocationRepository geolocationRepo,
            Cacher<User, string> user,
            Cacher<OutputInventoryDto, string> inventoryCacher)
        {
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
            _userContext = userContext;
            _searcher = searcher;
            _apiTokenProvider = apiTokenProvider;
            _geolocationRepo = geolocationRepo;
            _userCache = user;
            _inventoryCacher = inventoryCacher;
        }

        public async IAsyncEnumerable<OutputInventoryDto> GetInventories([EnumeratorCancellation]CancellationToken cancellation,InputSearchInventoryDto? input = null)
        {
            foreach (var user in await GetUsersFromCity(input, cancellation)) // юзеры с дб
            {
                foreach (var inventory in await GetOutputInventories(input, user, cancellation))
                {
                    yield return inventory;
                }
            }
        }


        #region help methods for GetInventories

        async Task<IEnumerable<User>> GetUsersFromCity(InputSearchInventoryDto? input, CancellationToken cancellation)
        {
            string city = 
                input?.City ?? 
                await GetUserCity(input?.Lat, input?.Lon, cancellation) ?? 
                "москва";

            return _userCache.Cache(city, () => SelectByCity(city));
        }
        IEnumerable<User> SelectByCity(string city) => _userContext.Users.Where(u => u.City == city);

        
        


        async Task<string?> GetUserCity(double? lat, double? lon, CancellationToken cancellation)
        {
            string? city = null;
            if (lat == null || lon == null)
                return city;
            try
            {
                city = (await _geolocationRepo.GetUserLocationByLatLon(lat.Value, lon.Value, cancellation)).City;
            }
            finally {}
            return city;
        }
        // юзер с дб
        async Task<string> GetToken(User user, CancellationToken cancellation)  => await _apiTokenProvider.GetTokenFrom(user.Password, user.Login);//токен береться из AuthApi по логину и паролю
        
        async Task<IEnumerable<OutputInventoryDto>> GetOutputInventories(InputSearchInventoryDto? input, User user, CancellationToken cancellation)
        {
            try
            {
                if (IsCaching(input))
                    return await _inventoryCacher.CacheAsync(user.Login, () => CacheSource(input, user, cancellation));
                else
                    return await TryGetOutputInventories(input, user, cancellation);
            }
            catch
            {
            }
            return Enumerable.Empty<OutputInventoryDto>();
        }

        async IAsyncEnumerable<OutputInventoryDto> CacheSource(InputSearchInventoryDto? input, User user, [EnumeratorCancellation]CancellationToken cancellation)
        {
            foreach (var inventory in await TryGetOutputInventories(input, user, cancellation))
            {
                yield return inventory;
            }
        }


        async Task<IEnumerable<OutputInventoryDto>> TryGetOutputInventories(InputSearchInventoryDto? input, User user, CancellationToken cancellation)
        {
            var HIRAInput = _mapper.Map<InputHIRAInventoryDto>(input);
            var token = await GetToken(user, cancellation);
            var HIRAInventoriesResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HIRAInput); // запрос и ответ от апи
            if (HIRAInventoriesResult.Array != null && HIRAInventoriesResult.Array.Count > 0) // чтобы не передать пустой массив метод для поиска тегов
            {
                var inventoriesResultDto = _mapper.Map<OutputInventoriesResultDto>(HIRAInventoriesResult).Array;
                var byRrice = FilterByPrice(inventoriesResultDto, input);
                var byTag =  _searcher.SelectInventoriesByTags(input?.Tags, byRrice);
                return byTag;
            }
            return Enumerable.Empty<OutputInventoryDto>();
        }
        IEnumerable<OutputInventoryDto> FilterByPrice(IEnumerable<OutputInventoryDto> inventories, InputSearchInventoryDto? input)
        {
            int minPrice = GetMinPrice(inventories, input);
            var maxPrice = GetMaxPrice(inventories, input);
            return inventories.Where(i => WherePriceExpression(i, minPrice, maxPrice));
        }

        bool WherePriceExpression(OutputInventoryDto inve, int minPrice, int maxPrice)
        {
            var invetoryFalse = inve.Prices.Any(i => i.Values.Any(v => v.Value < minPrice)) || inve.Prices.Any(i => i.Values.Any(v => v.Value > maxPrice));
            return !invetoryFalse;
        }

        


        int GetMinPrice(IEnumerable<OutputInventoryDto> inventories, InputSearchInventoryDto? input)
        {
            var minValue = inventories.Select(i => i.Prices.MinBy(p => p.Values.MinBy(u => u.Value))).Select(u => u?.Values.MinBy(u => u.Value)).Select(v => v?.Value).Min() ?? 0;


            int minPrice;
            if (input?.MinPrice != null && input?.MinPrice >= minValue)
                minPrice = input.MinPrice.Value;
            else
                minPrice = minValue;
            return minPrice;
        }
        int GetMaxPrice(IEnumerable<OutputInventoryDto> inventories, InputSearchInventoryDto? input)
        {
            var maxValue = inventories.Select(i => i.Prices.MaxBy(p => p.Values.MaxBy(u => u.Value))).Select(u => u?.Values.MaxBy(v => v.Value)).Select(v => v?.Value).Max() ?? int.MaxValue;


            int maxPrice;
            if (input?.MaxPrice != null && input?.MaxPrice <= maxValue)
                maxPrice = input.MaxPrice.Value;
            else
                maxPrice = maxValue;
            return maxPrice;
        }


        bool IsCaching(InputSearchInventoryDto? input)
        {
            if (input == null || SpecifyFieldsAreNull(input))
                return true;
            return false;
        }
        bool SpecifyFieldsAreNull(InputSearchInventoryDto input)
        {
            if (input.Discounts == null && (input.Tags == null || input.Tags.Length == 0)
                && input.Limit == null && input.Offset == null && input.RentNumber == null 
                && input.Search == null && input.StateId == null && input.Title == null 
                && input.Description == null && input.MaxPrice == null && input.MinPrice == null)
                return true;
            return false;
        }
        
        #endregion



    }
}
