using Web.Dtos.Sales.Inventory;
using apiInventoryDto = HendInRentApi.Dto.Inventory;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using AutoMapper;
using DataBase;
using Web.Cryptographer;
using Web.Search.Inventory;
using HendInRentApi.Dto.Inventory;

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
        public SaleService(HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> inventoryRepo,
            IMapper mapper, 
            UserContext userContext, ICryptographer cryptographer, 
            InventoryTagSearcher searcher,
            ApiTokenProvider apiTokenProvider)
        {
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
            _userContext = userContext;
            _cryptographer = cryptographer;
            _searcher = searcher;
            _apiTokenProvider = apiTokenProvider;
        }


        public async Task<OutputInventoriesResultDto> GetInventory(string token, InputSearchInventoryDto? input = null)
        {
            var HERAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            var apiResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HERAInput);

            var res = _mapper.Map<OutputInventoriesResultDto>(apiResult);
            return res;
        }

        public async IAsyncEnumerable<OutputInventoriesResultDto> GetInventories(InputSearchInventoryDto? input = null)
        {
            var HIRAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            await foreach (var token in GetTokensByLogin())
            {
                var res = await GetOutputInventoriesResult(input, HIRAInput, token);
                if (res != null)
                    yield return res;   
            }
        }



        #region help methods for GetInventories
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
            if (tags != null)
                inventories.Array = _searcher.TagsIsContained(tags, inventories.Array).ToList();
        }

        async IAsyncEnumerable<string> GetTokensByLogin()
        {
            foreach (var u in _userContext.Users)
            {
                var encrpyptedPass = u.Password;
                var p = _cryptographer.Decrypt(encrpyptedPass);
                var l = u.Login;
                var token = await _apiTokenProvider.GetToken(p, l);
                yield return token;
            }   
        }
        #endregion



    }
}
