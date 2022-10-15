using HendInRentApi;
using FluentValidation;
using Web.Models;
using Web.Models.ModelValidators;
using Web.Services;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Web.HasingToken;
using Web.PasswordHasher;
using Web.Geolocation;

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
            services.AddScoped<IValidator<UserLoginModel>, UserLoginModelValidator>();
            return services;
        }
        public static IServiceCollection AddNativeServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddTransient<TokenCryptographer, TokenCryptographerImpl>();
            services.AddTransient<IPasswordHasher, PasswordHasherImpl>();
            services.AddTransient<GeolocationRepository>();
            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, ConfigurationManager configuration)
        {
            var strCon = configuration.GetConnectionString("Sqlite");
            services.AddDbContext<UserContext>(cfg => cfg.UseSqlite(strCon));
            return services;
        }
    }


}
