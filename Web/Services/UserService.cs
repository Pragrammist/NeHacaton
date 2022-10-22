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
        readonly AuthRentInHendApi _authApi;
        readonly IMapper _mapper;

        readonly UserContext _userContext;
        readonly ITokenCryptographer _tokenCryptographer;
        readonly IPasswordHasher _passwordHasher;
        readonly GeolocationRepository _geolocation;

        public UserService(AuthRentInHendApi authApi, IMapper mapper, UserContext userContext, ITokenCryptographer tokenCryptographer, 
            IPasswordHasher passwordHasher, GeolocationRepository geolocation)
        {
            _geolocation = geolocation;
            _mapper = mapper;
            _authApi = authApi;
            _userContext = userContext;
            _tokenCryptographer = tokenCryptographer;
            _passwordHasher = passwordHasher;
        }
        /// <summary>
        /// //inputUser must be already validate
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public async Task<OutputUserDto> RegistrateUser(InputUserRegistrationDto inputUser)
        {
            var token = await GetUserToken(inputUser);

            var user = await GetUserEntity(inputUser, token);

            _userContext.Add(user);

            await _userContext.SaveChangesAsync();

            var outPutUser = _mapper.Map<OutputUserDto>(user);

            return outPutUser;
        }



        #region help methods for reg
        //incapsulate creating user entity
        async Task<User> GetUserEntity(InputUserRegistrationDto inputUser, Token token)
        {
            var userToWriteInDb = _mapper.Map<User>(inputUser);

            userToWriteInDb.Token = token;

            userToWriteInDb.Password = _passwordHasher.Hash(inputUser.Password);

            var location = await GetLocation(inputUser);

            userToWriteInDb.City = location.City;

            return userToWriteInDb;
        }

        async Task<OutputLocationDto> GetLocation(InputUserRegistrationDto user)
        {
            return await _geolocation.GetUserLocationByLatLon(user.Lat, user.Lon);
        }

        async Task<Token> GetUserToken(InputUserRegistrationDto inputUser)
        {
            var toLoginDto = _mapper.Map<InputHERALoginUserRentInHendDto>(inputUser);

            var authorizedUserTokenDto = await _authApi.Login(toLoginDto);

            var token = _mapper.Map<Token>(authorizedUserTokenDto);

            token.AccessTokenHash = _tokenCryptographer.Encrypt(authorizedUserTokenDto.AccessToken);

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
