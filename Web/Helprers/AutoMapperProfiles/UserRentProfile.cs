using Api = HendInRentApi.Dto.SelfInfo.Rent;
using Service = Web.Dtos.UserSelfInfoDto.Rent;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class RentSelfInfoProfile : Profile
    {
        public RentSelfInfoProfile()
        {
            CreateMap<Service.InputRentSearchDto, Api.InputHIRARentSearchDto>();
            CreateMap<Api.OutputHIRAAdminDto, Service.OutputAdminDto>();
            CreateMap<Api.OutputHIRAInnerInventoryDto, Service.OutputInnerInventoryDto>();
            CreateMap<Api.OutputHIRAInventoryDto, Service.OutputInventoryDto>();
            CreateMap<Api.OutputHIRAOptionDto, Service.OutputOptionDto>();
            CreateMap<Api.OutputHIRAPermissionDto, Service.OutputPermissionDto>();
            CreateMap<Api.OutputHIRAPointDto, Service.OutputPointDto>();
            CreateMap<Api.OutputHIRAPriceDto, Service.OutputPriceDto>();
            CreateMap<Api.OutputHIRARentDto, Service.OutputRentDto>();
            CreateMap<Api.OutputHIRARentsResultDto, Service.OutputRentResultDto>();
            CreateMap<Api.OutputHIRARentStateDto, Service.OutputRentStateDto>();
            CreateMap<Api.OutputHIRAResourceDto, Service.OutputResourceDto>();
            CreateMap<Api.OutputHIRAStateDto, Service.OutputStateDto>();
            CreateMap<Api.OutputHIRAValueDto, Service.OutputValueDto>();
        }
    }
}
