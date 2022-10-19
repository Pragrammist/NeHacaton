using Web.Dtos.Sales.Inventory;
using apiDto = HendInRentApi.Dto.Inventory;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using AutoMapper;

namespace Web.Services
{
    public class SaleService 
    {
        GenericRepositoryApi _genericRepositoryApi;
        IMapper _mapper;
        public SaleService(GenericRepositoryApi genericRepositoryApi, IMapper mapper)
        {
            _genericRepositoryApi = genericRepositoryApi;
            _mapper = mapper;
        }


        public async Task<OutputInventoriesDto> GetInventory(string token, InputInventoryDto? input = null)
        {
            return await GetInventoryPrivate(token, input);
        }
        private async Task<OutputInventoriesDto> GetInventoryPrivate(string token, InputInventoryDto? input = null) // it's tempory method. In future it will deleted
        {
            var inp = _mapper.Map<apiDto.InputInventoryDto>(input);

            var apiResult = await _genericRepositoryApi.MakePostJsonTypeRequest<apiDto.OutputInventoriesDto, apiDto.InputInventoryDto>(POST_INVENTORY_ITEMS, token, inp);

            var res = _mapper.Map<OutputInventoriesDto>(apiResult);
            return res;
        }

        //public async Task<IEnumerable<OutputInventoriesDto>> GetInventiries(string token, InputInventoryDto? input = null, int countUser) //todo more more performancly with less mapping
        //{

        //}

    }
}
