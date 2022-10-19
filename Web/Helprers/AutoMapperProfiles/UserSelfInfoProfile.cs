using ApiProfile = HendInRentApi.Dto.SelfInfo.Profile;
using WebProfile = Web.Dtos.UserSelfInfoDto.Profile;

using AutoMapper;

namespace Web.Helprers.AutoMapperProfiles
{
    public class UserSelfInfoProfile : Profile
    {
        public UserSelfInfoProfile()
        {
            CreateMap<ApiProfile.OutputPermissionSelfInfoDto, WebProfile.OutputPermissionSelfInfoDto>();
            CreateMap<ApiProfile.OutputProfileSelfIonfoDto, WebProfile.OutputProfileSelfIonfoDto>();
            CreateMap<ApiProfile.OutputSelfInfoProfileApiResultDto, WebProfile.OutputSelfInfoProfileResultDto>();
        }
    }
}
