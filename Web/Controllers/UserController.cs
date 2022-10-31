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
using System.ComponentModel.DataAnnotations;

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
        InputUserRegistrationDto GetRegUser(UserRegistrationModel userRegModel)
        {
            var inputUser = _mapper.Map<InputUserRegistrationDto>(userRegModel);
            inputUser.City = GetCity();
            return inputUser;
        }
        string? GetCity()
        {
            string? city;
            HttpContext.Request.Cookies.TryGetValue("city", out city);
            return city;
        }
        IActionResult GetValidationStatusCode(FluentValidation.Results.ValidationResult validationRes)
        {
            if (validationRes.Errors.Any(u => u.ErrorCode == "404"))
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLoginModel userLoginModel)
        {
            var valRes = await _userLoginValidator.ValidateAsync(userLoginModel);

            if (!valRes.IsValid)
                return NotFound("Такого пользователя нет");

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

            var claims = new List<Claim>() { new Claim(CLAIM_PASSWORD, $"{user.Password}"), new Claim(ClaimsIdentity.DefaultNameClaimType, $"{user.Login}") };
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
