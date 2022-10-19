using ApiDto = HendInRentApi.Dto.Inventory;
using WebDto =  Web.Dtos.Sales.Inventory;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            CreateMap<WebDto.InputDiscountDto, ApiDto.InputDiscountDto>();
            CreateMap<WebDto.InputDiscountsDto, ApiDto.InputDiscountsDto>();
            CreateMap<WebDto.InputInventoryDto, ApiDto.InputInventoryDto>();
            CreateMap<ApiDto.OutputInventoriesDto, WebDto.OutputInventoriesDto>();
            CreateMap<ApiDto.OutputInventoryDto, WebDto.OutputInventoryDto>();
            CreateMap<ApiDto.OutputOptionDto, WebDto.OutputOptionDto>();
            CreateMap<ApiDto.OutputPermissionDto, WebDto.OutputPermissionDto>();
            CreateMap<ApiDto.OutputPointDto, WebDto.OutputPointDto>();
            CreateMap<ApiDto.OutputPriceDto, WebDto.OutputPriceDto>();
            CreateMap<ApiDto.OutputResourceDto, WebDto.OutputResourceDto>();
            CreateMap<ApiDto.OutputStateDto, WebDto.OutputStateDto>();
            CreateMap<ApiDto.OutputValueDto, WebDto.OutputValueDto>();
        }
    }
}
