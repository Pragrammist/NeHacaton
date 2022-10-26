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
            var HIRAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            await foreach (var u in _userContext.Users)
            {
                var inventories = await FilerByCityAndGetOutputInventories(u, input, HIRAInput);
                foreach (var inventory in inventories)
                {
                    yield return inventory;
                }
            }
        }



        #region help methods for GetInventories
        async Task<IEnumerable<OutputInventoryDto>> FilerByCityAndGetOutputInventories(User user, InputSearchInventoryDto? input, InputHIRAInventoryDto HIRAInput)
        {
            if (!await FilterByCity(user, input?.Lat, input?.Lon))
                return new OutputInventoryDto[] { };

            var token = await GetToken(user);
            var res = await GetOutputInventories(input, HIRAInput, token);
            return res;
        }

        async Task<IEnumerable<OutputInventoryDto>> GetOutputInventories(InputSearchInventoryDto? input, InputHIRAInventoryDto? HIRAInput, string token)
        {
            var HIRAInventoriesResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HIRAInput);
            if (HIRAInventoriesResult.Array != null && HIRAInventoriesResult.Array.Count > 0)
            {
                var inventoriesResultDto = _mapper.Map<OutputInventoriesResultDto>(HIRAInventoriesResult);
                return _searcher.SelectInventoriesByTags(input?.Tags, inventoriesResultDto.Array);  
            }
            return new OutputInventoryDto[] { };
        }
        async Task<bool> FilterByCity(User user, double? lat, double? lon)
        {
            if (lat == null || lon == null)
                return true;

            var city = (await _geolocationRepo.GetUserLocationByLatLon(lat.Value, lon.Value)).City;

            if(user.City == city)
                return true;

            return false;
        }

        async Task<string> GetToken(User user)
        {
            var encrpyptedPass = user.Password;
            var p = _cryptographer.Decrypt(encrpyptedPass);
            var l = user.Login;
            var token = await _apiTokenProvider.GetToken(p, l);
            return token;
        }
        #endregion



    }
}
