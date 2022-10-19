using Api = HendInRentApi.Dto.SelfInfo.Rent;
using Service = Web.Dtos.UserSelfInfoDto.Rent;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class RentSelfInfoProfile : Profile
    {
        public RentSelfInfoProfile()
        {
            CreateMap<Service.InputRentSearchDto, Api.InputRentSearchDto>();
            CreateMap<Api.OutputAdminDto, Service.OutputAdminDto>();
            CreateMap<Api.OutputDiscountDto, Service.OutputDiscountDto>();
            CreateMap<Api.OutputInnerInventoryDto, Service.OutputInnerInventoryDto>();
            CreateMap<Api.OutputInventoryDto, Service.OutputInventoryDto>();
            CreateMap<Api.OutputOptionDto, Service.OutputOptionDto>();
            CreateMap<Api.OutputPermissionDto, Service.OutputPermissionDto>();
            CreateMap<Api.OutputPointDto, Service.OutputPointDto>();
            CreateMap<Api.OutputPriceDto, Service.OutputPriceDto>();
            CreateMap<Api.OutputRentDto, Service.OutputRentDto>();
            CreateMap<Api.OutputRentResultDto, Service.OutputRentResultDto>();
            CreateMap<Api.OutputRentStateDto, Service.OutputRentStateDto>();
            CreateMap<Api.OutputResourceDto, Service.OutputResourceDto>();
            CreateMap<Api.OutputStateDto, Service.OutputStateDto>();
            CreateMap<Api.OutputValueDto, Service.OutputValueDto>();
        }
    }
}
