using Web.Dtos.Sales.Inventory;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using AutoMapper;
using DataBase;
using Web.Cryptographer;
using Web.Search.Inventory;
using HendInRentApi.Dto.Inventory;
using DataBase.Entities;
using Web.Geolocation;
using System.Linq;

namespace Web.Services
{
    public class SaleService 
    {
        readonly HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> _inventoryRepo;
        readonly IMapper _mapper;
        readonly UserContext _userContext;
        readonly ICryptographer _cryptographer;
        readonly InventoryTagSearcher _searcher;
        readonly ApiTokenProvider _apiTokenProvider;
        readonly GeolocationRepository _geolocationRepo;
        public SaleService(
            HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> inventoryRepo,
            IMapper mapper, 
            UserContext userContext, ICryptographer cryptographer, 
            InventoryTagSearcher searcher,
            ApiTokenProvider apiTokenProvider,
            GeolocationRepository geolocationRepo)
        {
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
            _userContext = userContext;
            _cryptographer = cryptographer;
            _searcher = searcher;
            _apiTokenProvider = apiTokenProvider;
            _geolocationRepo = geolocationRepo;
        }


        public async IAsyncEnumerable<OutputInventoryDto> GetInventories(InputSearchInventoryDto? input = null)
        {
            foreach (var user in await GetUsersFromCity(input))
            {
                foreach (var inventory in await GetOutputInventories(input, user))
                {
                    yield return inventory;
                }
            }
        }



        #region help methods for GetInventories

        async Task<IEnumerable<User>> GetUsersFromCity(InputSearchInventoryDto? input) //TODO RENAME User ent
        {
            string? city = input?.City ?? await GetUserCity(input?.Lat, input?.Lon);
            return _userContext.Users.Where(u => city == null || u.City.ToLower() == city.ToLower());
        }
        async Task<string?> GetUserCity(double? lat, double? lon)
        {
            //TODO In cookies write lat and lon from client and get city in midleware
            //TODO Caching
            if (lat == null || lon == null)
                return null;
            return (await _geolocationRepo.GetUserLocationByLatLon(lat.Value, lon.Value)).City;
        }
        async Task<string> GetToken(User user)
        {
            var encrpyptedPass = user.Password;
            var p = _cryptographer.Decrypt(encrpyptedPass);
            var l = user.Login;
            var token = await _apiTokenProvider.GetToken(p, l);
            return token;
        }
        async Task<IEnumerable<OutputInventoryDto>> GetOutputInventories(InputSearchInventoryDto? input, User user)
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
        #endregion



    }
}
