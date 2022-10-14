using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        IValidator<UserRegistrationModel> _userRegistrationModelValidator;
        public UserController(IValidator<UserRegistrationModel> userRegistrationModelValidator)
        {
            _userRegistrationModelValidator = userRegistrationModelValidator;
        }

        [HttpPost]
        public IActionResult RegistrateUser([FromBody]UserRegistrationModel user)
        {
            var validationRes = _userRegistrationModelValidator.Validate(user);

            if (!validationRes.IsValid)
                return Json(validationRes.Errors);



            return Json(user);
        }


    }
}
