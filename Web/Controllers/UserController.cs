using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Dtos;
using Web.Models;
using Web.Services;
using static Web.Constants.ClaimConstants;
using static Web.Helprers.ControllerExtensions;
using Microsoft.AspNetCore.Http.Extensions;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        IValidator<UserRegistrationModel> _userRegistrationModelValidator;
        UserService _userService;
        IMapper _mapper;
        IValidator<UserLoginModel> _userLoginValidator;

        public UserController(
            IValidator<UserRegistrationModel> userRegistrationModelValidator, 
            UserService userService, 
            IMapper mapper, IValidator<UserLoginModel> userLoginValidator)
        {
            _userRegistrationModelValidator = userRegistrationModelValidator;
            _userService = userService;
            _mapper = mapper;
            _userLoginValidator = userLoginValidator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserRegistrationModel userRegModel)
        {
            var validationRes = await _userRegistrationModelValidator.ValidateAsync(userRegModel);
            
            if (!validationRes.IsValid)
                return GetValidationStatusCode(validationRes);

            var inputUser = GetRegUser(userRegModel);

            var user = await _userService.RegistrateUser(inputUser);

            await SignInAsync(user);
            
            return Json(user);
        }
        

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLoginModel userLoginModel)
        {
            var valRes = await _userLoginValidator.ValidateAsync(userLoginModel);

            if (!valRes.IsValid)
                return NotFound(valRes.Errors);

            var inputUser = _mapper.Map<InputLoginUserDto>(userLoginModel);

            var user = await _userService.LoginUser(inputUser);

            await SignInAsync(user);
            
            return Json(user); 
        }
        void SetAspAuthTokenToHeader()
        {
            const string ASP_AUTH_TOKEN = ".AspNetCore.Cookies";
            var cookies = HttpContext.Response.GetTypedHeaders().SetCookie;
            var cookie = cookies.First(u => u.Name == ASP_AUTH_TOKEN);
            HttpContext.Response.Headers.Add(ASP_AUTH_TOKEN, cookie.Value.ToString());
        }
        [HttpPost]
        public async Task<IActionResult> SignOutUser()
        {
            await SignOutAsync();

            return SignOut();
        }

        #region helper method
        InputUserRegistrationDto GetRegUser(UserRegistrationModel userRegModel)
        {
            var inputUser = _mapper.Map<InputUserRegistrationDto>(userRegModel);
            inputUser.City = this.GetCityFromHttpContext();
            return inputUser;
        }
        IActionResult GetValidationStatusCode(FluentValidation.Results.ValidationResult validationRes)
        {
            if (validationRes.Errors.Any(u => u.ErrorCode == "404"))
            {
                return NotFound(validationRes.Errors);
            }
            else
            {
                return BadRequest(validationRes.Errors);
            }
        }

        async Task SignInAsync(OutputUserDto user)
        {
            if (HttpContext == null)
                return;

            var claims = new List<Claim>() { new Claim(CLAIM_PASSWORD, $"{user.Password}"), new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Login}") };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            var claimPrincipal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
            SetAspAuthTokenToHeader();
        }

        async Task SignOutAsync()
        {
            if (HttpContext == null)
                return;

            await HttpContext.SignOutAsync();
        }
        #endregion
    }
}
