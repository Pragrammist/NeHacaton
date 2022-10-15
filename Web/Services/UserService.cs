using AutoMapper;
using DataBase;
using DataBase.Entities;
using HendInRentApi;
using Web.Dtos;
using Web.HasingToken;

namespace Web.Services
{
    public class UserService
    {
        readonly AuthRentInHendApi _authApi;
        readonly IMapper _mapper;
        UserContext _userContext;
        TokenCryptographer _tokenCryptographer;
        public UserService(AuthRentInHendApi authApi, IMapper mapper, UserContext userContext, TokenCryptographer tokenCryptographer)
        {
            _mapper = mapper;
            _authApi = authApi;
            _userContext = userContext;
            _tokenCryptographer = tokenCryptographer;
        }
        /// <summary>
        /// //user must be already validate
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RegistrateUser(InputUserRegistrationDto user)
        {
            var toLoginDto = _mapper.Map<InputLoginUserDto>(user);

            var authorizedUserTokenDto = await _authApi.Login(toLoginDto);

            var token = _mapper.Map<Token>(authorizedUserTokenDto);

            token.AccessTokenHash = _tokenCryptographer.Encrypt(authorizedUserTokenDto.AccessToken);

            var userToWriteInDb = _mapper.Map<User>(user);

            userToWriteInDb.Token = token;

            _userContext.Add(userToWriteInDb);

            await _userContext.SaveChangesAsync();
        }
    }
}
