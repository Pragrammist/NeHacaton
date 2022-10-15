using DataBase;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.PasswordHasher;

namespace Web.Models.ModelValidators
{
    public class UserLoginModelValidator : AbstractValidator<UserLoginModel>
    {
        UserContext _userContext;
        IPasswordHasher _passwordHasher;
        public UserLoginModelValidator(UserContext userContext, IPasswordHasher passwordHasher)
        {
            _userContext = userContext;
            _passwordHasher = passwordHasher;

            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.Login).NotNull();
            RuleFor(u => u).CustomAsync(CheckExistsUserInDb).When(u => u.Login != null && u.Password != null);
            

        }

        async Task CheckExistsUserInDb(UserLoginModel model, ValidationContext<UserLoginModel> validationContext, CancellationToken cancellationToken)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(u => 
            (u.Login == model.Login 
            || u.Email == model.Login 
            || u.Telephone == model.Login) 
            && u.Password == _passwordHasher.Hash(model.Password));

            if (user is null)
                validationContext.AddFailure("user", "user not found");
        }


    }
}
