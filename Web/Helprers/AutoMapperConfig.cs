

using AutoMapper;
using DataBase.Entities;
using HendInRentApi;
using Web.Dtos;
using Web.Dtos.Sales.Inventory;
using Web.HasingToken;
using Web.Helprers.AutoMapperProfiles;
using Web.Models;
using Web.Models.Inventory;

namespace Web.Helprers
{
    public static class AutoMapperConfig
    {

        public static IServiceCollection ConfigAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(ConfigeMapper);

            return services;
        }

        static void ConfigeMapper(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<UserSelfInfoProfile>();
            cfg.AddProfile<RentSelfInfoProfile>();
            cfg.AddProfile<InventoryProfile>();
            cfg.CreateMap<UserRegistrationModel, InputUserRegistrationDto>(); //model is validated, registration dto isn't.
            //It's just to pass UserService Reg method


            cfg.CreateMap<InputUserRegistrationDto, InputHIRALoginUserDto>();  //why is reg becoming login?
            //when users registrate here they login in RentInHend.

            cfg.CreateMap<OutputHIRAAuthTokenDto, Token>().ForMember(t => t.AccessTokenHash, cfg => cfg.Ignore());
            //token from api to db token

            cfg.CreateMap<InputUserRegistrationDto, User>().ForMember(t => t.Password, cfg => cfg.Ignore());
            //from dto model in UserService to user entity

            cfg.CreateMap<UserLoginModel, InputLoginUserDto>();

            cfg.CreateMap<Token, OutputTokenDto>();

            cfg.CreateMap<User, OutputUserDto>();

            cfg.CreateMap<InventorySearchModel, InputSearchInventoryDto>();
        }        
    }
}
