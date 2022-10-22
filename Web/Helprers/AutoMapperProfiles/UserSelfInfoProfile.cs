using ApiProfile = HendInRentApi.Dto.SelfInfo.Profile;
using WebProfile = Web.Dtos.UserSelfInfoDto.Profile;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class UserSelfInfoProfile : Profile
    {
        public UserSelfInfoProfile()
        {
            CreateMap<ApiProfile.OutputHERAPermissionSelfInfoDto, WebProfile.OutputPermissionSelfInfoDto>();
            CreateMap<ApiProfile.OutputHERAProfileSelfIonfoDto, WebProfile.OutputProfileSelfIonfoDto>();
            CreateMap<ApiProfile.OutputHERAProfileSelfInfoResultDto, WebProfile.OutputSelfInfoProfileResultDto>();
        }
    }
}
