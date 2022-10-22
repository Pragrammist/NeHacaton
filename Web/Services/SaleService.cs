using Web.Dtos.Sales.Inventory;
using apiInventoryDto = HendInRentApi.Dto.Inventory;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using AutoMapper;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Web.HasingToken;
using System.Linq;
using Web.Search.Inventory;
using HendInRentApi.Dto.Inventory;

namespace Web.Services
{
    public class SaleService 
    {
        HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> _inventoryRepo;
        IMapper _mapper;
        UserContext _userContext;
        ITokenCryptographer _cryptographer;
        InventoryTagSearcher _searcher;
        public SaleService(HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto> inventoryRepo,
            IMapper mapper, 
            UserContext userContext, ITokenCryptographer cryptographer, 
            InventoryTagSearcher searcher)
        {
            _inventoryRepo = inventoryRepo;
            _mapper = mapper;
            _userContext = userContext;
            _cryptographer = cryptographer;
            _searcher = searcher;
        }


        public async Task<OutputInventoriesResultDto> GetInventory(string token, InputSearchInventoryDto? input = null)
        {
            var HERAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            var apiResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HERAInput);

            var res = _mapper.Map<OutputInventoriesResultDto>(apiResult);
            return res;
        }

        public async Task<IEnumerable<OutputInventoriesResultDto>> GetInventories(InputSearchInventoryDto? input = null)
        {
            var inventories = new LinkedList<OutputInventoriesResultDto>();
            var HIRAInput = _mapper.Map<InputHIRAInventoryDto>(input);

            foreach (var token in DecryptedTokens())
            {
                await FillInventoryList(input, inventories, HIRAInput, token);
            }

            return inventories;            
        }



        #region help methods for GetInventories
        async Task FillInventoryList(InputSearchInventoryDto? input, LinkedList<OutputInventoriesResultDto> list, InputHIRAInventoryDto? HIRAInput, string token)
        {
            try
            {
                var HIRAInventoriesResult = await _inventoryRepo.MakePostJsonTypeRequest(POST_INVENTORY_ITEMS, token, HIRAInput);
                AddInventoriesResultToList(list, HIRAInventoriesResult, input?.Tags);
            }
            catch
            {

            }
        }
        void AddInventoriesResultToList(LinkedList<OutputInventoriesResultDto> list, OutputHIRAInventoriesResultDto HIRAInventories, string[]? tags)
        {
            if (HIRAInventories.Array != null && HIRAInventories.Array.Count > 0)
            {
                var inventories = _mapper.Map<OutputInventoriesResultDto>(HIRAInventories);
                SelectInventoriesByTags(tags, inventories);
                list.AddLast(inventories);
            }
        }

        void SelectInventoriesByTags(string[]? tags, OutputInventoriesResultDto inventories)
        {
            if (tags != null)
                inventories.Array = _searcher.TagsIsContained(tags, inventories.Array).ToList();
        }

        IEnumerable<string> DecryptedTokens() => _userContext.Tokens.Select(t => _cryptographer.Decrypt(t.AccessTokenHash));
        #endregion



    }
}
