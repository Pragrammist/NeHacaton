using AutoMapper;
using DataBase;
using DataBase.Entities;
using Web.Dtos;
using Web.Geolocation;
using Web.Cryptography;
using HendInRentApi.Dto.SelfInfo.Profile;
using HendInRentApi;
using static HendInRentApi.RentInHendApiConstants;
using DataBase.Extensions;

namespace Web.Services
{
    public class UserService
    {
        readonly IMapper _mapper;
        readonly UserContext _userContext;
        readonly ICryptographer _passwordCryptographer;
        readonly GeolocationRepository _geolocation;
        readonly HIRARepository<OutputHIRAProfileSelfInfoResultDto> _profileRepo;
        readonly ApiTokenProvider _tokenProvider;
        public UserService(
            IMapper mapper, 
            UserContext userContext, 
            ICryptographer passwordCryptographer, 
            GeolocationRepository geolocation,
            HIRARepository<OutputHIRAProfileSelfInfoResultDto> profileRepo,
            ApiTokenProvider tokenProvider)
        {
            _geolocation = geolocation;
            _mapper = mapper;
            _userContext = userContext;
            _passwordCryptographer = passwordCryptographer;
            _profileRepo = profileRepo;
            _tokenProvider = tokenProvider;
        }
        /// <summary>
        /// //regUser must be already validate
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public async Task<OutputUserDto> RegistrateUser(InputUserRegistrationDto inputUser)
        {
            var user = await CreateUserEntity(inputUser); //todo someservice maybe

            _userContext.Add(user);

            await _userContext.SaveChangesAsync();

            var outputUser = await GetUserDto(user, inputUser.Password, inputUser.Login);

            return outputUser;
        }

        public async Task<OutputUserDto> LoginUser(InputLoginUserDto inputUserLoginDto)
        {
            var user = await _userContext.Users.FindUserByAsync(inputUserLoginDto.Login) ?? throw new InvalidOperationException("user not found");

            var outputUser = await GetUserDto(user, inputUserLoginDto.Password, inputUserLoginDto.Login);

            return outputUser;
        }

        #region help methods for reg
        async Task<OutputUserDto> GetUserDto(User user, string password, string login)
        {
            var outPutUser = _mapper.Map<OutputUserDto>(user);
            var profile = await GetHiraProfile(password, login);
            outPutUser.Fio = profile.Fio;
            outPutUser.Avatar = profile.Avatar;
            return outPutUser;
        }



        //incapsulate creating user entity
        async Task<User> CreateUserEntity(InputUserRegistrationDto regUser)
        {
            var user = _mapper.Map<User>(regUser);

            user.Password = _passwordCryptographer.Encrypt(regUser.Password);

            user.City = user.City ?? await GetLocationCity(regUser);

            return user;
        }

        async Task<string> GetLocationCity(InputUserRegistrationDto user) => (await _geolocation.GetUserLocationByLatLon(user.Lat, user.Lon)).City;

        
        async Task<OutputHIRAProfileSelfIonfoDto> GetHiraProfile(string password, string login)
        {
            var token = await _tokenProvider.GetToken(password, login);

            var profile = (await _profileRepo.MakePostJsonTypeRequest(POST_PROFILE, token)).Array.First();
            return profile;
        }
        #endregion
    }
}
