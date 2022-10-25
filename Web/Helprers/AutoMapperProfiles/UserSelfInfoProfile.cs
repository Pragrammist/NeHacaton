using ApiProfile = HendInRentApi.Dto.SelfInfo.Profile;
using WebProfile = Web.Dtos.UserSelfInfoDto.Profile;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class UserSelfInfoProfile : Profile
    {
        public UserSelfInfoProfile()
        {
            CreateMap<ApiProfile.OutputHIRAPermissionSelfInfoDto, WebProfile.OutputPermissionSelfInfoDto>();
            CreateMap<ApiProfile.OutputHIRAProfileSelfIonfoDto, WebProfile.OutputProfileDto>();
            CreateMap<ApiProfile.OutputHIRAProfileSelfInfoResultDto, WebProfile.OutputProfileResultDto>();
        }
    }
}
