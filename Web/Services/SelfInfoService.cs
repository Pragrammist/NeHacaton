using AutoMapper;
using HendInRentApi;
using HendInRentApi.Dto.SelfInfo.Profile;
using Web.Dtos.UserSelfInfoDto.Profile;
using Web.Dtos.UserSelfInfoDto.Rent;
using static HendInRentApi.RentInHendApiConstants;
using HendInRentApi.Dto.SelfInfo.Rent;
using DataBase;
using DataBase.Entities;
using Web.Dtos;
using DataBase.Extensions;
using Microsoft.AspNetCore.Identity;
using Web.Geolocation;

namespace Web.Services
{
    public class SelfInfoService
    {
        readonly HIRARepository<OutputHIRAProfileSelfInfoResultDto> _profileRepo;
        readonly HIRARepository<OutputHIRARentsResultDto, InputHIRARentSearchDto> _rentRepo;
        readonly IMapper _mapper;
        readonly UserContext _userContext;
        readonly GeolocationRepository _geolocation;
        public SelfInfoService(
            IMapper mapper, 
            HIRARepository<OutputHIRAProfileSelfInfoResultDto> profileRepo, 
            HIRARepository<OutputHIRARentsResultDto, InputHIRARentSearchDto> rentRepo,
            UserContext userContext,
            GeolocationRepository geolocation)
        {
            _mapper = mapper;
            _profileRepo = profileRepo;
            _rentRepo = rentRepo;
            _userContext = userContext;
            _geolocation = geolocation;
        }
        public async Task<IEnumerable<OutputRentDto>> GetUserRent(string token, InputRentSearchDto? inputRentSerchDto = null)
        {
            inputRentSerchDto = inputRentSerchDto ?? new InputRentSearchDto { }; // если в апи засунуть null объект то будет искоючение
            
            
            var HEARInput = _mapper.Map<InputHIRARentSearchDto>(inputRentSerchDto);
            var apiRes = await _rentRepo.MakePostJsonTypeRequest(POST_RENT,token, HEARInput);
            var res = _mapper.Map<OutputRentResultDto>(apiRes);
            return res.Array;
        }

        public async Task<OutputProfileResultDto> GetUserProfile(string token, string login)
        {
            var selfInfoApiResult = await _profileRepo.MakePostJsonTypeRequest(POST_PROFILE, token);
            var res = _mapper.Map<OutputProfileResultDto>(selfInfoApiResult);
            var profile = res.Array.First();
            res.User = await GetUserDtoBy(login, profile.Fio, profile.Avatar);
            return res;
        }


        public async Task<OutputUserDto> ChangeCity(double lat, double lon, string login)
        {
            var city = await _geolocation.GetUserLocationByLatLon(lat, lon);
            return await ChangeCityPrivate(city.City, login);
        }
        private async Task<OutputUserDto> ChangeCityPrivate(string city, string login)
        {
            var entityUser = await FindUserBy(login);
            entityUser.ChangeCity(city);
            await _userContext.SaveChangesAsync();
            var outputUser = _mapper.Map<OutputUserDto>(entityUser);
            return outputUser;
        }
        public async Task<OutputUserDto> ChangeCity(string city, string login)
        {
            return await ChangeCityPrivate(city, login);
        }

        
        async Task<OutputUserDto> GetUserDtoBy(string login, string fio, string avatar)
        {
            var userEnt = await FindUserBy(login);
            var res = _mapper.Map<OutputUserDto>(userEnt);
            res.Avatar = avatar;
            res.Fio = fio;
            return res;
        }
        async Task<User> FindUserBy(string login) => await _userContext.Users.FindUserByAsync(login) ?? throw new InvalidOperationException("User not found");
    }
}
