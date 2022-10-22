using Api = HendInRentApi.Dto.SelfInfo.Rent;
using Service = Web.Dtos.UserSelfInfoDto.Rent;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class RentSelfInfoProfile : Profile
    {
        public RentSelfInfoProfile()
        {
            CreateMap<Service.InputRentSearchDto, Api.InputHERARentSearchDto>();
            CreateMap<Api.OutputHERAAdminDto, Service.OutputAdminDto>();
            CreateMap<Api.OutputHERAInnerInventoryDto, Service.OutputInnerInventoryDto>();
            CreateMap<Api.OutputHERAInventoryDto, Service.OutputInventoryDto>();
            CreateMap<Api.OutputHERAOptionDto, Service.OutputOptionDto>();
            CreateMap<Api.OutputHERAPermissionDto, Service.OutputPermissionDto>();
            CreateMap<Api.OutputHERAPointDto, Service.OutputPointDto>();
            CreateMap<Api.OutputHERAPriceDto, Service.OutputPriceDto>();
            CreateMap<Api.OutputHERARentDto, Service.OutputRentDto>();
            CreateMap<Api.OutputHERARentsResultDto, Service.OutputRentResultDto>();
            CreateMap<Api.OutputHERARentStateDto, Service.OutputRentStateDto>();
            CreateMap<Api.OutputHERAResourceDto, Service.OutputResourceDto>();
            CreateMap<Api.OutputHERAStateDto, Service.OutputStateDto>();
            CreateMap<Api.OutputHERAValueDto, Service.OutputValueDto>();
        }
    }
}
