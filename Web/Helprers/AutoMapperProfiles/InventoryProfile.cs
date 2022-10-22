using ApiDto = HendInRentApi.Dto.Inventory;
using WebDto =  Web.Dtos.Sales.Inventory;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<WebDto.InputDiscountDto, ApiDto.InputHIRADiscountDto>();
            CreateMap<WebDto.InputDiscountsDto, ApiDto.InputHIRADiscountsDto>();
            CreateMap<WebDto.InputSearchInventoryDto, ApiDto.InputHIRAInventoryDto>();
            CreateMap<ApiDto.OutputHIRAInventoriesResultDto, WebDto.OutputInventoriesResultDto>();
            CreateMap<ApiDto.OutputHIRAInventoryDto, WebDto.OutputInventoryDto>();
            CreateMap<ApiDto.OutputHIRAOptionDto, WebDto.OutputOptionDto>();
            CreateMap<ApiDto.OutputHIRAPermissionDto, WebDto.OutputPermissionDto>();
            CreateMap<ApiDto.OutputHIRAPointDto, WebDto.OutputPointDto>();
            CreateMap<ApiDto.OutputHIRAPriceDto, WebDto.OutputPriceDto>();
            CreateMap<ApiDto.OutputHIRAResourceDto, WebDto.OutputResourceDto>();
            CreateMap<ApiDto.OutputHIRAStateDto, WebDto.OutputStateDto>();
            CreateMap<ApiDto.OutputHIRAValueDto, WebDto.OutputValueDto>();
        }
    }
}
