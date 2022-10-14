using FluentValidation;


namespace Web.Models.ModelValidators
{
    public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationModelValidator()
        {
            RuleFor(c => c).NotNull().ChildRules(validator => 
            {
                validator.RuleFor(t => t.Password).NotNull().WithErrorCode("422").WithMessage("cannot be null"); ;
                validator.RuleFor(t => t.Login).NotNull().WithErrorCode("422").WithMessage("cannot be null"); ;
                validator.RuleFor(t => t.City).NotNull().WithErrorCode("422").WithMessage("cannot be null"); ;
            }).WithMessage("user cannot be null").WithName("user").WithErrorCode("422");
        }
    }
}
