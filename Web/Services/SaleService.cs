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
using Microsoft.EntityFrameworkCore;

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

        public async IAsyncEnumerable<OutputInventoryDto> GetInventories(InputSearchInventoryDto? input = null)
        {
            foreach (var user in await GetUsersFromCity(input)) // юзеры с дб
            {
                foreach (var inventory in await GetOutputInventories(input, user))
                {
                    yield return inventory;
                }
            }
        }


        #region help methods for GetInventories

        async Task<IEnumerable<User>> GetUsersFromCity(InputSearchInventoryDto? input)
        {
            string city = 
                input?.City ?? 
                await GetUserCity(input?.Lat, input?.Lon) ?? 
                "москва";

            return _userCache.Cache(city, () => SelectByCity(city));
        }
        IEnumerable<User> SelectByCity(string city) => _userContext.Users.Where(u => u.City == city);

        
        


        async Task<string?> GetUserCity(double? lat, double? lon)
        {
            string? city = null;
            if (lat == null || lon == null)
                return city;
            try
            {
                city = (await _geolocationRepo.GetUserLocationByLatLon(lat.Value, lon.Value)).City;
            }
            finally {}
            return city;
        }
        // юзер с дб
        async Task<string> GetToken(User user)  => await _apiTokenProvider.GetTokenFrom(user.Password, user.Login);//токен береться из AuthApi по логину и паролю
        
        async Task<IEnumerable<OutputInventoryDto>> GetOutputInventories(InputSearchInventoryDto? input, User user)
        {
            try
            {
                if (IsCaching(input))
                    return await _inventoryCacher.CacheAsync(user.Login, () => CacheSource(input, user));
                else
                    return await TryGetOutputInventories(input, user);
            }
            catch
            {
            }
            return Enumerable.Empty<OutputInventoryDto>();
        }

        async IAsyncEnumerable<OutputInventoryDto> CacheSource(InputSearchInventoryDto? input, User user)
        {
            foreach (var inventory in await TryGetOutputInventories(input, user))
            {
                yield return inventory;
            }
        }


        async Task<IEnumerable<OutputInventoryDto>> TryGetOutputInventories(InputSearchInventoryDto? input, User user)
        {
            var HIRAInput = _mapper.Map<InputHIRAInventoryDto>(input);
            var token = await GetToken(user);
            var HIRAInventoriesResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HIRAInput); // запрос и ответ от апи
            if (HIRAInventoriesResult.Array != null && HIRAInventoriesResult.Array.Count > 0) // чтобы не передать пустой массив метод для поиска тегов
            {
                var inventoriesResultDto = _mapper.Map<OutputInventoriesResultDto>(HIRAInventoriesResult);
                return _searcher.SelectInventoriesByTags(input?.Tags, inventoriesResultDto.Array);
            }
            return Enumerable.Empty<OutputInventoryDto>();
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
                && input.Description == null)
                return true;
            return false;
        }
        
        #endregion



    }
}
