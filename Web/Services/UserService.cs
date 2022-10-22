using AutoMapper;
using DataBase;
using DataBase.Entities;
using HendInRentApi;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Web.Dtos;
using Web.Geolocation;
using Web.HasingToken;
using Web.PasswordHasher;

namespace Web.Services
{
    public class UserService
    {
        readonly IMapper _mapper;

        readonly UserContext _userContext;
        readonly ITokenCryptographer _tokenCryptographer;
        readonly IPasswordHasher _passwordHasher;
        readonly GeolocationRepository _geolocation;
        readonly HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> _loginHIRA;
        public UserService(HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> loginHIRA, IMapper mapper, UserContext userContext, ITokenCryptographer tokenCryptographer, 
            IPasswordHasher passwordHasher, GeolocationRepository geolocation)
        {
            _geolocation = geolocation;
            _mapper = mapper;
            _loginHIRA = loginHIRA;
            _userContext = userContext;
            _tokenCryptographer = tokenCryptographer;
            _passwordHasher = passwordHasher;
        }
        /// <summary>
        /// //regUser must be already validate
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public async Task<OutputUserDto> RegistrateUser(InputUserRegistrationDto inputUser)
        {
            var user = await CreateUserEntity(inputUser);

            _userContext.Add(user);

            await _userContext.SaveChangesAsync();

            var outPutUser = _mapper.Map<OutputUserDto>(user);

            return outPutUser;
        }



        #region help methods for reg
        //incapsulate creating user entity
        async Task<User> CreateUserEntity(InputUserRegistrationDto regUser)
        {
            var user = _mapper.Map<User>(regUser);

            user.Token = await GetUserToken(regUser);

            user.Password = _passwordHasher.Hash(regUser.Password);

            user.City = await GetLocationCity(regUser);

            return user;
        }

        async Task<string> GetLocationCity(InputUserRegistrationDto user)
        {
            return (await _geolocation.GetUserLocationByLatLon(user.Lat, user.Lon)).City;
        }

        async Task<Token> GetUserToken(InputUserRegistrationDto inputUser)
        {
            var HIRALogin = _mapper.Map<InputHIRALoginUserDto>(inputUser);

            var HIRAToken = await _loginHIRA.Login(HIRALogin);

            var token = _mapper.Map<Token>(HIRAToken);

            token.AccessTokenHash = _tokenCryptographer.Encrypt(HIRAToken.AccessToken);

            return token;
        }
        #endregion


        public async Task<OutputUserDto> LoginUser(InputLoginUserDto inputUserLoginDto)
        {
            var user = await _userContext.Users.Include(u => u.Token).FirstAsync(u => u.Email == inputUserLoginDto.Login ||
            u.Login == inputUserLoginDto.Login ||
            u.Telephone == inputUserLoginDto.Login);

            var outPutUser = _mapper.Map<OutputUserDto>(user);

            return outPutUser;
        }
    }
}
