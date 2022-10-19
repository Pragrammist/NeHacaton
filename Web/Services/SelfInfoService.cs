using AutoMapper;
using HendInRentApi;
using HendInRentApi.Dto.SelfInfo.Profile;
using Web.Dtos.UserSelfInfoDto.Profile;
using Web.Dtos.UserSelfInfoDto.Rent;
using static HendInRentApi.RentInHendApiConstants;
using RentApi = HendInRentApi.Dto.SelfInfo.Rent;

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
        public async Task<OutputRentResultDto> GetUserRentSelfInfo(string token, InputRentSearchDto inputRentSerchDto = null)
        {
            var inp = _mapper.Map<RentApi.InputRentSearchDto>(inputRentSerchDto);

            var apiRes = await _repositoryApi.MakePostJsonTypeRequest<RentApi.OutputRentResultDto,RentApi.InputRentSearchDto>(POST_RENT,token, inp);
            var res = _mapper.Map<OutputRentResultDto>(apiRes);
            return res;
        }

        public async Task<OutputSelfInfoProfileResultDto> GetUserProfileSelfInfo(string token)
        {
            var selfInfoApiResult = await _repositoryApi.MakePostJsonTypeRequest<OutputSelfInfoProfileApiResultDto>(POST_PROFILE, token);

            var res = _mapper.Map<OutputSelfInfoProfileResultDto>(selfInfoApiResult);

            return res;
        }

    }
}
