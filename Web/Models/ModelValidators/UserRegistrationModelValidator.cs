using DataBase;
using FluentValidation;
using HendInRentApi;
using Microsoft.EntityFrameworkCore;


namespace Web.Models.ModelValidators
{
    public class UserRegistrationModelValidator : AbstractValidator<UserRegistrationModel>
    {
        public UserRegistrationModelValidator(AuthRentInHendApi api, UserContext userContext)
        {
            var rendInHendValidator = new UserExistsInRendInHendValidator(api);
            var notRegistratedValidator = new UserIsNotRegistratedInMarketplace(userContext);
            RuleFor(u => u).NotNull().ChildRules(validator => 
            {
                validator.RuleFor(t => t.Password).NotNull().WithErrorCode("422").WithMessage("cannot be null"); 
                validator.RuleFor(t => t.Login).NotNull().WithErrorCode("422").WithMessage("cannot be null");
                validator.RuleFor(t => t.Lat).InclusiveBetween(-180, 180);
                validator.RuleFor(t => t.Lon).InclusiveBetween(-180, 180);
                validator.RuleFor(t => t.Email).NotNull().WithErrorCode("422").WithMessage("cannot be null");
                validator.RuleFor(t => t.Telephone).NotNull().WithErrorCode("422").WithMessage("cannot be null");
            }).WithMessage("user cannot be null").WithName("user").WithErrorCode("422");


            RuleFor(u => u)
                .CustomAsync(rendInHendValidator.ValidateFields)
                .When(FieldsNotNullCondition)
                .CustomAsync(notRegistratedValidator.ValidateFields)
                .When(FieldsNotNullCondition);
        }

        bool FieldsNotNullCondition(UserRegistrationModel m) => m.Telephone != null && m.Login != null && m.Email != null && m.Password != null;


        #region additional classes for validation

        class UserExistsInRendInHendValidator
        {
            AuthRentInHendApi _api;

            
            public UserExistsInRendInHendValidator(AuthRentInHendApi api)
            {
                _api = api;
                
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
                    context.AddFailure(model.Email, emailMessage);

                if (!string.IsNullOrEmpty(telephoneMessage))
                    context.AddFailure(model.Telephone, telephoneMessage);

                if (!string.IsNullOrEmpty(loginMessage))
                    context.AddFailure(model.Login, loginMessage);

            }

            #region field checks
            async Task<string> CheckField(string password, string login)
            {
                string message = String.Empty;
                try
                {
                    var inpUser = new InputLoginUserRentInHendDto { Login = login, Password = password };
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
                    context.AddFailure(model.Login, "login already exists");

                var emailNotExists = await EmailNotExists(model);

                if (!emailNotExists)
                    context.AddFailure(model.Email, "email already exists");

                var telephoneNotExists = await TelephoneNotExists(model);

                if (!telephoneNotExists)
                    context.AddFailure(model.Telephone, "telephone already exists");
            }



            #region existing fields checking
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
