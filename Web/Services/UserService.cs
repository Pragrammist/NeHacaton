using AutoMapper;
using DataBase;
using DataBase.Entities;
using HendInRentApi;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Web.Dtos;
using Web.Geolocation;
using Web.Cryptographer;
using Web.PasswordHasher;

namespace Web.Services
{
    public class UserService
    {
        readonly IMapper _mapper;

        readonly UserContext _userContext;
        readonly ICryptographer _passwordCryptographer;
        readonly IPasswordHasher _passwordHasher;
        readonly GeolocationRepository _geolocation;
        readonly HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> _loginHIRA;
        public UserService(HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> loginHIRA, IMapper mapper, UserContext userContext, ICryptographer passwordCryptographer, 
            IPasswordHasher passwordHasher, GeolocationRepository geolocation)
        {
            _geolocation = geolocation;
            _mapper = mapper;
            _loginHIRA = loginHIRA;
            _userContext = userContext;
            _passwordCryptographer = passwordCryptographer;
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

            user.Password = _passwordCryptographer.Encrypt(regUser.Password);

            user.City = await GetLocationCity(regUser);

            return user;
        }

        async Task<string> GetLocationCity(InputUserRegistrationDto user)
        {
            return (await _geolocation.GetUserLocationByLatLon(user.Lat, user.Lon)).City;
        }

        #endregion


        public async Task<OutputUserDto> LoginUser(InputLoginUserDto inputUserLoginDto)
        {
            var user = await _userContext.Users.FirstAsync(
                u => 
                u.Email == inputUserLoginDto.Login ||
                u.Login == inputUserLoginDto.Login ||
                u.Telephone == inputUserLoginDto.Login
            );

            var outPutUser = _mapper.Map<OutputUserDto>(user);

            return outPutUser;
        }
    }
}
