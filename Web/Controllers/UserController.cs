using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Web.Dtos;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        IValidator<UserRegistrationModel> _userRegistrationModelValidator;
        UserService _userService;
        IMapper _mapper;
        public UserController(IValidator<UserRegistrationModel> userRegistrationModelValidator, UserService userService, IMapper mapper)
        {
            _userRegistrationModelValidator = userRegistrationModelValidator;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrateUser([FromBody]UserRegistrationModel user)
        {
            var validationRes = _userRegistrationModelValidator.Validate(user);

            if (!validationRes.IsValid)
                return Json(validationRes.Errors);

            var inputUser = _mapper.Map<InputUserRegistrationDto>(user);

            await _userService.RegistrateUser(inputUser);

            return Json(user);
        }


    }
}
