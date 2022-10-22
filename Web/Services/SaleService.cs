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


        public async Task<OutputInventoriesResultDto> GetInventory(string token, InputSearchInventoryDto? input = null)
        {
            var HERAInput = _mapper.Map<InputHERAInventoryDto>(input);

            var apiResult = await _genericRepositoryApi.MakePostJsonTypeRequest<OutputHERAInventoriesResultDto,InputHERAInventoryDto>(POST_INVENTORY_ITEMS, token, HERAInput);

            var res = _mapper.Map<OutputInventoriesResultDto>(apiResult);
            return res;
        }

        public async Task<IEnumerable<OutputInventoriesResultDto>> GetInventories(InputSearchInventoryDto? input = null)
        {
            var inventories = new LinkedList<OutputInventoriesResultDto>();
            var HERAInput = _mapper.Map<InputHERAInventoryDto>(input);

            foreach (var token in DecryptedTokens())
            {
                await FillInventoryList(input, inventories, HERAInput, token);
            }

            return inventories;            
        }



        #region help methods for GetInventories
        async Task FillInventoryList(InputSearchInventoryDto? input, LinkedList<OutputInventoriesResultDto> list, InputHERAInventoryDto? HERAInput, string token)
        {
            try
            {
                var HERAInventoriesResult = await _genericRepositoryApi.MakePostJsonTypeRequest
                    <OutputHERAInventoriesResultDto, InputHERAInventoryDto>(POST_INVENTORY_ITEMS, token, HERAInput);
                AddInventoriesResultToList(list, HERAInventoriesResult, input?.Tags);
            }
            catch
            {

            }
        }
        void AddInventoriesResultToList(LinkedList<OutputInventoriesResultDto> list, OutputHERAInventoriesResultDto HERAInventories, string[]? tags)
        {
            if (HERAInventories.Array != null && HERAInventories.Array.Count > 0)
            {
                var inventories = _mapper.Map<OutputInventoriesResultDto>(HERAInventories);
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
