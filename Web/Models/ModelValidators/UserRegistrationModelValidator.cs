using DataBase;
using FluentValidation;
using FluentValidation.Results;
using HendInRentApi;
using Microsoft.EntityFrameworkCore;


namespace Web.Models.ModelValidators
{
    public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationModelValidator(HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> api, UserContext userContext)
        {
            var rendInHendValidator = new UserExistsInRendInHendValidator(api);
            var notRegistratedValidator = new UserIsNotRegistratedInMarketplace(userContext);
            RuleFor(u => u).NotNull().ChildRules(validator => 
            {
                validator.RuleFor(t => t.Password).NotNull().NotEmpty().WithErrorCode("404").WithMessage("cannot be null"); 
                validator.RuleFor(t => t.Login).NotNull().NotEmpty().WithErrorCode("404").WithMessage("cannot be null");
                validator.RuleFor(t => t.Lat).InclusiveBetween(-180, 180).WithErrorCode("404");
                validator.RuleFor(t => t.Lon).InclusiveBetween(-180, 180).WithErrorCode("404");
                validator.RuleFor(t => t.Email).NotNull().NotEmpty().WithErrorCode("404").WithMessage("cannot be null");
                validator.RuleFor(t => t.Telephone).NotNull().NotEmpty().WithErrorCode("404").WithMessage("cannot be null");
            }).WithMessage("user cannot be null").WithName("user").WithErrorCode("404");


            RuleFor(u => u)
                .CustomAsync(rendInHendValidator.ValidateFields)
                .When(FieldsNotNullCondition)
                .CustomAsync(notRegistratedValidator.ValidateFields)
                .When(FieldsNotNullCondition);
        }

        bool FieldsNotNullCondition(UserRegistrationModel m) => !string.IsNullOrEmpty(m.Telephone) && !string.IsNullOrEmpty(m.Login) && !string.IsNullOrEmpty(m.Email) && !string.IsNullOrEmpty(m.Password);

        #region additional classes for validation
        class UserExistsInRendInHendValidator
        {
            HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> _api;

            
            public UserExistsInRendInHendValidator(HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto> loginHIRA)
            {
                _api = loginHIRA;


            }

            
            public async Task ValidateFields(UserRegistrationModel model, ValidationContext<UserRegistrationModel> context, CancellationToken token)
            {
                await SetErrors(model, context);
            }

            async Task SetErrors(UserRegistrationModel model, ValidationContext<UserRegistrationModel> context)
            {
                var emailMessage = await CheckEmail(model);
                var telephoneMessage = await CheckTelephone(model);
                var loginMessage = await CheckLogin(model);

                if (!string.IsNullOrEmpty(emailMessage))
                    context.AddFailure(GetFailure(nameof(model.Email), emailMessage, "404"));

                if (!string.IsNullOrEmpty(telephoneMessage))
                    context.AddFailure(GetFailure(nameof(model.Telephone), telephoneMessage, "404"));

                if (!string.IsNullOrEmpty(loginMessage))
                    context.AddFailure(GetFailure(nameof(model.Login), loginMessage, "404"));

            }

            ValidationFailure GetFailure(string propName, string message, string code) 
                => new ValidationFailure {PropertyName = propName, ErrorMessage = message, ErrorCode = code };

            #region field checks
            async Task<string> CheckField(string password, string login)
            {
                string message = String.Empty;
                try
                {
                    var inpUser = new InputHIRALoginUserDto { Login = login, Password = password };
                    await _api.Login(inpUser);
                }
                catch (HttpRequestException ex)
                {
                    message = ex.Message;
                }
                return message;
            }

            async Task<string> CheckEmail(UserRegistrationModel model)
            {
                return await CheckField(model.Password, model.Email);
            }
            async Task<string> CheckTelephone(UserRegistrationModel model)
            {
                return await CheckField(model.Password, model.Telephone);
            }
            async Task<string> CheckLogin(UserRegistrationModel model)
            {
                return await CheckField(model.Password, model.Login);
            }

            #endregion
        }

        class UserIsNotRegistratedInMarketplace 
        {
            UserContext _userContext;
            public UserIsNotRegistratedInMarketplace(UserContext userContext)
            {
                _userContext = userContext;
                
            }
            public async Task ValidateFields(UserRegistrationModel model, ValidationContext<UserRegistrationModel> context, CancellationToken token)
            {
                var loginNotExists = await LoginNotExists(model);

                if (!loginNotExists)
                    context.AddFailure(GetFailure(nameof(model.Login), "login already exists", "400"));

                var emailNotExists = await EmailNotExists(model);

                if (!emailNotExists)
                    context.AddFailure(GetFailure(nameof(model.Email), "email already exists", "400"));

                var telephoneNotExists = await TelephoneNotExists(model);

                if (!telephoneNotExists)
                    context.AddFailure(GetFailure(nameof(model.Telephone), "telephone already exists", "400"));
            }



            #region existing fields checking

            ValidationFailure GetFailure(string propName, string message, string code)
                => new ValidationFailure { PropertyName = propName, ErrorMessage = message, ErrorCode = code };

            async Task<bool> EmailNotExists(UserRegistrationModel model)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                return user is null;
            }
            async Task<bool> LoginNotExists(UserRegistrationModel model)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Login == model.Login);
                return user is null;
            }

            async Task<bool> TelephoneNotExists(UserRegistrationModel model)
            {
                var user = await _userContext.Users.FirstOrDefaultAsync(u => u.Telephone == model.Telephone);
                return user is null;
            }
            #endregion
        }

        #endregion
    }
}
