using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Dtos;
using Web.Models;
using Web.Services;
using static Web.Constants.ClaimConstants;

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


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult RegistrateUser()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrateUser([FromBody]UserRegistrationModel userRegModel)
        {
            var validationRes = await _userRegistrationModelValidator.ValidateAsync(userRegModel);

            if (!validationRes.IsValid)
                return Json(validationRes.Errors);

            var inputUser = _mapper.Map<InputUserRegistrationDto>(userRegModel);

            var user = await _userService.RegistrateUser(inputUser);

            await SignInAsync(user);

            return Json(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(UserLoginModel userLoginModel)
        {
            var valRes = await _userLoginValidator.ValidateAsync(userLoginModel);

            if (!valRes.IsValid)
                return Json(valRes.Errors);

            var inputUser = _mapper.Map<InputLoginUserDto>(userLoginModel);

            var user = await _userService.LoginUser(inputUser);

            await SignInAsync(user);
            
            return Json(user);

        }

        

        public async Task<IActionResult> SignOutUser()
        {
            await SignOutAsync();

            return SignOut();
        }

        async Task SignInAsync(OutputUserDto user)
        {
            if (HttpContext == null)
                return;

            var claims = new List<Claim>() { new Claim(RENTINHEND_API_TOKEN_CLAIM, $"{user.Token.AccessTokenHash}"), new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Login}") };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            var claimPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
            
        }

        async Task SignOutAsync()
        {
            if (HttpContext == null)
                return;

            await HttpContext.SignOutAsync();
        }

    }
}
