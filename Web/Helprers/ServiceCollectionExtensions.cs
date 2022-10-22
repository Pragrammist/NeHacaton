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
using Web.Search.Inventory;
using HendInRentApi.Dto.Inventory;
using HendInRentApi.Dto.SelfInfo.Rent;
using HendInRentApi.Dto.SelfInfo.Profile;

namespace Web.Helprers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRentInHendApiServices(this IServiceCollection services)
        {
            services.AddTransient<HIRALogin<OutputHIRAAuthTokenDto, InputHIRALoginUserDto>, AuthRentInHendApi>();
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
            services.AddTransient<ITokenCryptographer, TokenCryptographerImpl>();
            services.AddScoped<SelfInfoService>();
            services.AddScoped<SaleService>();
            services.AddTransient<IPasswordHasher, PasswordHasherImpl>();
            services.AddTransient<GeolocationRepository>();
            services.AddTransient<InventoryTagSearcher, InventoryTagSearcherImpl>();
            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, ConfigurationManager configuration)
        {
            var strCon = configuration.GetConnectionString("Sqlite");
            services.AddDbContext<UserContext>(cfg => cfg.UseSqlite(strCon));
            return services;
        }

        public static IServiceCollection AddApiRepositrories(this IServiceCollection services)
        {
            services.AddTransient<
                HIRARepository<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto>, 
                GenericRepositoryApi<OutputHIRAInventoriesResultDto, InputHIRAInventoryDto>>();

            services.AddTransient<
                HIRARepository<OutputHIRARentsResultDto, InputHIRARentSearchDto>, 
                GenericRepositoryApi<OutputHIRARentsResultDto, InputHIRARentSearchDto>>();

            services.AddTransient<HIRARepository<OutputHIRAProfileSelfInfoResultDto>, GenericRepositoryApi<OutputHIRAProfileSelfInfoResultDto>>();
            return services;
        }
    }


}
