using AutoMapper;
using HendInRentApi;
using HendInRentApi.Dto.SelfInfo.Profile;
using Web.Dtos.UserSelfInfoDto.Profile;
using Web.Dtos.UserSelfInfoDto.Rent;
using static HendInRentApi.RentInHendApiConstants;
using HendInRentApi.Dto.SelfInfo.Rent;
using DataBase;
using DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Web.Dtos;

namespace Web.Services
{
    public class SelfInfoService
    {
        HIRARepository<OutputHIRAProfileSelfInfoResultDto> _profileRepo;
        HIRARepository<OutputHIRARentsResultDto, InputHIRARentSearchDto> _rentRepo;
        IMapper _mapper;
        UserContext _userContext;
        public SelfInfoService(IMapper mapper, 
            HIRARepository<OutputHIRAProfileSelfInfoResultDto> profileRepo, 
            HIRARepository<OutputHIRARentsResultDto, InputHIRARentSearchDto> rentRepo,
            UserContext userContext)
        {
            _mapper = mapper;
            _profileRepo = profileRepo;
            _rentRepo = rentRepo;
            _userContext = userContext;
        }
        public async Task<OutputRentResultDto> GetUserRent(string token, InputRentSearchDto? inputRentSerchDto = null)
        {
            inputRentSerchDto = inputRentSerchDto ?? new InputRentSearchDto { };


            var HEARInput = _mapper.Map<InputHIRARentSearchDto>(inputRentSerchDto);
            var apiRes = await _rentRepo.MakePostJsonTypeRequest(POST_RENT,token, HEARInput);
            var res = _mapper.Map<OutputRentResultDto>(apiRes);
            return res;
        }

        public async Task<OutputSelfInfoProfileResultDto> GetUserProfile(string token, string login)
        {
            var selfInfoApiResult = await _profileRepo.MakePostJsonTypeRequest(POST_PROFILE, token);
            var res = _mapper.Map<OutputSelfInfoProfileResultDto>(selfInfoApiResult);
            res.User = await GetUserDtoBy(login);

            return res;
        }

        public async Task<OutputUserDto> ChangeCity(string city, string login)
        {
            var entityUser = await FindUserByLogin(login);
            entityUser.ChangeCity(city);
            await _userContext.SaveChangesAsync();
            var outputUser = _mapper.Map<OutputUserDto>(entityUser);
            return outputUser;
        }


        async Task<OutputUserDto> GetUserDtoBy(string login)
        {
            var userEnt = await FindUserByLogin(login);
            return _mapper.Map<OutputUserDto>(userEnt);
        }
        async Task<User> FindUserByLogin(string login) => await _userContext.Users.FirstAsync(u => u.Email == login || u.Telephone == login || u.Login == login);
    }
}
