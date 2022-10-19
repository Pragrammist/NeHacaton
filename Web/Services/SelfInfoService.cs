using AutoMapper;
using HendInRentApi;
using HendInRentApi.Dto.SelfInfo.Profile;
using Web.Dtos.UserSelfInfoDto.Profile;
using Web.Dtos.UserSelfInfoDto.Rent;
using static HendInRentApi.RentInHendApiConstants;

namespace Web.Services
{
    public class SelfInfoService
    {
        GenericRepositoryApi _repositoryApi;
        IMapper _mapper;
        public SelfInfoService(GenericRepositoryApi repositoryApi, IMapper mapper)
        {
            _repositoryApi = repositoryApi;
            _mapper = mapper;
        }
        //public OutputUserRentSelfInfoDto GetUserRentSelfInfo(InputRentSerchDto inputRentSerchDto)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<OutputSelfInfoProfileResultDto> GetUserProfileSelfInfo(string token)
        {
            var selfInfoApiResult = await _repositoryApi.MakePostJsonTypeRequest<OutputSelfInfoProfileApiResultDto>(POST_PROFILE, token);

            var res = _mapper.Map<OutputSelfInfoProfileResultDto>(selfInfoApiResult);

            return res;
        }

    }
}
