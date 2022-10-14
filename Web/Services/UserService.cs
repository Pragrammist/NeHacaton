using AutoMapper;
using DataBase;
using DataBase.Entities;
using HendInRentApi;
using Web.Dtos;


namespace Web.Services
{
    public class UserService
    {
        readonly AuthRentInHendApi _authApi;
        readonly IMapper _mapper;
        UserContext _userContext;
        public UserService(AuthRentInHendApi authApi, IMapper mapper, UserContext userContext)
        {
            _mapper = mapper;
            _authApi = authApi;
            _userContext = userContext;
        }

        public async Task RegistrateUser(InputUserRegistrationDto user)
        {
            var toLoginDto = _mapper.Map<InputLoginUserDto>(user);

            var authorizedUserTokenDto = await _authApi.Login(toLoginDto);

            var token = _mapper.Map<Token>(authorizedUserTokenDto);


        }
    }
}
