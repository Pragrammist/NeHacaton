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
        GenericRepositoryApi _genericRepositoryApi;
        IMapper _mapper;
        UserContext _userContext;
        ITokenCryptographer _cryptographer;
        InventoryTagSearcher _searcher;
        public SaleService(GenericRepositoryApi genericRepositoryApi, IMapper mapper, UserContext userContext, ITokenCryptographer cryptographer, InventoryTagSearcher searcher)
        {
            _genericRepositoryApi = genericRepositoryApi;
            _mapper = mapper;
            _userContext = userContext;
            _cryptographer = cryptographer;
            _searcher = searcher;
        }


        public async Task<OutputInventoriesDto> GetInventory(string token, InputSearchInventoryDto? input = null)
        {
            var inp = _mapper.Map<apiInventoryDto.InputInventoryDto>(input);

            var apiResult = await _genericRepositoryApi.MakePostJsonTypeRequest<apiInventoryDto.OutputInventoriesResultDto, apiInventoryDto.InputInventoryDto>(POST_INVENTORY_ITEMS, token, inp);

            var res = _mapper.Map<OutputInventoriesDto>(apiResult);
            return res;
        }

        public async Task<IEnumerable<OutputInventoriesDto>> GetInventories(InputSearchInventoryDto? input = null)
        {
            var outInventories = new LinkedList<OutputInventoriesDto>();
            var mapedInput = _mapper.Map<InputInventoryDto>(input);

            foreach (var token in DecryptedTokens())
            {
                await FillInventoryList(input, outInventories, mapedInput, token);
            }

            return outInventories;            
        }



        #region help methods for GetInventories
        private async Task FillInventoryList(InputSearchInventoryDto? input, LinkedList<OutputInventoriesDto> inventoryList, InputInventoryDto mapedInput, string token)
        {
            try
            {
                var inventoriesResult = await _genericRepositoryApi.MakePostJsonTypeRequest
                    <OutputInventoriesResultDto, InputInventoryDto>(POST_INVENTORY_ITEMS, token, mapedInput);
                AddInventoriesResultToList(inventoryList, inventoriesResult, input?.Tags);
            }
            catch
            {

            }
        }
        private void AddInventoriesResultToList(LinkedList<OutputInventoriesDto> list, OutputInventoriesResultDto inventories, string[]? tags)
        {
            if (inventories.Array != null && inventories.Array.Count > 0)
            {
                var mapInventories = _mapper.Map<OutputInventoriesDto>(inventories);
                SelectInventoriesByTags(tags, mapInventories);
                list.AddLast(mapInventories);
            }
        }

        void SelectInventoriesByTags(string[]? tags, OutputInventoriesDto inventories)
        {
            if (tags != null)
                inventories.Array = inventories.Array.Where(i => _searcher.TagsIsContained(tags, i.Description) 
                || _searcher.TagsIsContained(tags, i.Title)).ToList();
        }

        IEnumerable<string> DecryptedTokens() => _userContext.Tokens.Select(t => _cryptographer.Decrypt(t.AccessTokenHash));
        #endregion



    }
}
