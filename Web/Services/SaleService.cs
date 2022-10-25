using Web.Dtos.Sales.Inventory;
using apiInventoryDto = HendInRentApi.Dto.Inventory;
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
        HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> _inventoryRepo;
        IMapper _mapper;
        UserContext _userContext;
        ICryptographer _cryptographer;
        InventoryTagSearcher _searcher;
        ApiTokenProvider _apiTokenProvider;
        GeolocationRepository _geolocationRepo;
        public SaleService(HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> inventoryRepo,
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


        public async Task<OutputInventoriesResultDto> GetInventory(string token, InputSearchInventoryDto? input = null)
        {
            input = input ?? new InputSearchInventoryDto { };

            var HERAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            var apiResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HERAInput);

            var res = _mapper.Map<OutputInventoriesResultDto>(apiResult);
            return res;
        }

        public async IAsyncEnumerable<OutputInventoryDto> GetInventories(InputSearchInventoryDto? input = null)
        {
            var HIRAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            await foreach (var u in _userContext.Users)
            {
                var result = await CheckUserAndGetOutputInventoriesResult(u, input, HIRAInput);
                if(result != null)
                {
                    foreach (var inventory in result.Array)
                    {
                        yield return inventory;
                    }
                }
            }
        }



        #region help methods for GetInventories
        async Task<OutputInventoriesResultDto?> CheckUserAndGetOutputInventoriesResult(User user, InputSearchInventoryDto? input, InputHIRAInventoryDto HIRAInput)
        {
            if (!await CheckUserCity(user, input?.Lat, input?.Lon))
                return null;

            var token = await GetToken(user);
            var res = await GetOutputInventoriesResult(input, HIRAInput, token);
            return res;
        }


        IEnumerable<OutputInventoryDto> OutputInventoryArray(OutputInventoriesResultDto inputResult)
        {
            foreach (var inventory in inputResult.Array)
            {
                yield return inventory;
            }
        }


        async Task<OutputInventoriesResultDto?> GetOutputInventoriesResult(InputSearchInventoryDto? input, InputHIRAInventoryDto? HIRAInput, string token)
        {
            var HIRAInventoriesResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HIRAInput);
            OutputInventoriesResultDto? inventories = null;
            if (HIRAInventoriesResult.Array != null && HIRAInventoriesResult.Array.Count > 0)
            {
                inventories = _mapper.Map<OutputInventoriesResultDto>(HIRAInventoriesResult);
                SelectInventoriesByTags(input?.Tags, inventories);
                return inventories;
            }
            return inventories;
        }


        void SelectInventoriesByTags(string[]? tags, OutputInventoriesResultDto inventories)
        {
            if (tags != null && tags.Length == 0)
                inventories.Array = _searcher.TagsIsContained(tags, inventories.Array).ToList();
        }

        async Task<bool> CheckUserCity(User user, double? lat, double? lon)
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
