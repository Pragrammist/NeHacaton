using ApiDto = HendInRentApi.Dto.Inventory;
using WebDto =  Web.Dtos.Sales.Inventory;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<WebDto.InputDiscountDto, ApiDto.InputHERADiscountDto>();
            CreateMap<WebDto.InputDiscountsDto, ApiDto.InputHERADiscountsDto>();
            CreateMap<WebDto.InputSearchInventoryDto, ApiDto.InputHERAInventoryDto>();
            CreateMap<ApiDto.OutputHERAInventoriesResultDto, WebDto.OutputInventoriesDto>();
            CreateMap<ApiDto.OutputHERAInventoryDto, WebDto.OutputInventoryDto>();
            CreateMap<ApiDto.OutputHERAOptionDto, WebDto.OutputOptionDto>();
            CreateMap<ApiDto.OutputHERAPermissionDto, WebDto.OutputPermissionDto>();
            CreateMap<ApiDto.OutputHERAPointDto, WebDto.OutputPointDto>();
            CreateMap<ApiDto.OutputHERAPriceDto, WebDto.OutputPriceDto>();
            CreateMap<ApiDto.OutputHERAResourceDto, WebDto.OutputResourceDto>();
            CreateMap<ApiDto.OutputHERAStateDto, WebDto.OutputStateDto>();
            CreateMap<ApiDto.OutputHERAValueDto, WebDto.OutputValueDto>();
        }
    }
}
