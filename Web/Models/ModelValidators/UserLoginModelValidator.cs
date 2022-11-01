using DataBase;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Cryptography;
using DataBase.Extensions;

namespace Web.Models.ModelValidators
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        UserContext _userContext;
        ICryptographer _cryptographer;

        public UserLoginModelValidator(UserContext userContext, ICryptographer cryptographer)
        {
            _userContext = userContext;
            _cryptographer = cryptographer;

            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.Login).NotNull();
            RuleFor(u => u).CustomAsync(CheckExistsUserInDb).When(u => u.Login != null && u.Password != null);
            RuleFor(u => u.Password).NotNull().NotEmpty();
            RuleFor(u => u.Login).NotNull().NotEmpty();
            RuleFor(u => u).CustomAsync(CheckExistsUserInDb).When(u => !string.IsNullOrEmpty(u.Login) && !string.IsNullOrEmpty(u.Password));
        }

        async Task CheckExistsUserInDb(UserLoginModel model, ValidationContext<UserLoginModel> validationContext, CancellationToken cancellationToken)
        {
            var encrPassword = _cryptographer.Encrypt(model.Password);
            var user = await _userContext.Users.FindUserByAsync(model.Login, encrPassword);

            if (user is null)
                validationContext.AddFailure("user", "user not found");
        }
    }
}
