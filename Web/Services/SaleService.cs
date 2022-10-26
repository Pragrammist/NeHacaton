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
                 // я бы с радостью как-то убрал бы этот цикл в метод
                 // но для того, чтобы сделать вывод с помщью yield return, нужна эта вложенность
                 // поэтому все тело перенесено в FilerByCityAndGetOutputInventories
                 // чтобы код более или менее чистым был
                {
                    yield return inventory;
                }
            }
        }



        #region help methods for GetInventories
        // метод просто переносит логику из цикла foreach в GetInventories,
        async Task<IEnumerable<OutputInventoryDto>> FilerByCityAndGetOutputInventories(User user, InputSearchInventoryDto? input, InputHIRAInventoryDto HIRAInput)
        {
            if (!await FilterByCity(user, input?.Lat, input?.Lon))
                return new OutputInventoryDto[] { }; // выводиться пустой массив а не null, чтобы не сломать foreach

            var token = await GetToken(user);
            var res = await GetOutputInventories(input, HIRAInput, token);
            return res;
        }

        async Task<IEnumerable<OutputInventoryDto>> GetOutputInventories(InputSearchInventoryDto? input, InputHIRAInventoryDto? HIRAInput, string token)
        {
            var HIRAInventoriesResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HIRAInput); // запрос и ответ от апи
            if (HIRAInventoriesResult.Array != null && HIRAInventoriesResult.Array.Count > 0) // чтобы не передать пустой массив метод для поиска тегов
            {
                var inventoriesResultDto = _mapper.Map<OutputInventoriesResultDto>(HIRAInventoriesResult);
                return _searcher.SelectInventoriesByTags(input?.Tags, inventoriesResultDto.Array);  
            }
            return new OutputInventoryDto[] { };
        }
        async Task<bool> FilterByCity(User user, double? lat, double? lon) 
        // ну фильтр - громко сказано, просто дает true или fakse 
        // при сравнивании города из user(это арендодатель) и координатов пользователя решивший посмотреть каталог
        // TODO сделать запоминание города хранилище
        // TODO переменовать класс User
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
