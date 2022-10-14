using HendInRentApi;
using FluentValidation;
using Web.Models;
using Web.Models.ModelValidators;

namespace Web.Helprers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRentInHendApiServices(this IServiceCollection services)
        {
            services.AddTransient<AuthRentInHendApi>();
            services.AddTransient<GenericRepositoryApi>();
            return services;
        }

        public static IServiceCollection AddModelVlidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserRegistrationModel>, UserRegistrationModelValidator>();
            return services;
        }
    }


}
