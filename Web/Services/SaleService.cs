using Web.Dtos.Sales.Inventory;
using apiInventoryDto = HendInRentApi.Dto.Inventory;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using AutoMapper;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Web.HasingToken;
using System.Linq;

namespace Web.Services
{
    public class SaleService 
    {
        GenericRepositoryApi _genericRepositoryApi;
        IMapper _mapper;
        UserContext _userContext;
        ITokenCryptographer _cryptographer;
        public SaleService(GenericRepositoryApi genericRepositoryApi, IMapper mapper, UserContext userContext, ITokenCryptographer cryptographer)
        {
            _genericRepositoryApi = genericRepositoryApi;
            _mapper = mapper;
            _userContext = userContext;
            _cryptographer = cryptographer;
        }


        public async Task<OutputInventoriesDto> GetInventory(string token, InputInventoryDto? input = null)
        {
            return await GetInventoryPrivate(token, input);
        }
        private async Task<OutputInventoriesDto> GetInventoryPrivate(string token, InputInventoryDto? input = null) // it's tempory method. In future it will deleted
        {
            var inp = _mapper.Map<apiInventoryDto.InputInventoryDto>(input);

            var apiResult = await _genericRepositoryApi.MakePostJsonTypeRequest<apiInventoryDto.OutputInventoriesDto, apiInventoryDto.InputInventoryDto>(POST_INVENTORY_ITEMS, token, inp);

            var res = _mapper.Map<OutputInventoriesDto>(apiResult);
            return res;
        }

        public async Task<IEnumerable<OutputInventoriesDto>> GetInventories(InputInventoryDto? input = null)
        {
            LinkedList<OutputInventoriesDto> res = new LinkedList<OutputInventoriesDto>();
            var tokens = DecryptedTokens();
            var mapInput = _mapper.Map<apiInventoryDto.InputInventoryDto>(input);

            foreach (var token in tokens)
            {
                var inventories = await _genericRepositoryApi.MakePostJsonTypeRequest
                    <apiInventoryDto.OutputInventoriesDto, apiInventoryDto.InputInventoryDto>
                    (POST_INVENTORY_ITEMS, token, mapInput);

                if (inventories.Array != null && inventories.Array.Count > 0)
                {
                    var mapInventories = _mapper.Map<OutputInventoriesDto>(inventories);
                    res.AddLast(mapInventories);
                }
            }

            return res;            
        }

        IEnumerable<string> DecryptedTokens()
        {
            var decryptedTokens = _userContext.Tokens.Select(t => _cryptographer.Decrypt(t.AccessTokenHash));
            return decryptedTokens;
        }

       

    }
}
