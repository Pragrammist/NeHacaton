

using AutoMapper;
using DataBase.Entities;
using HendInRentApi;
using Web.Dtos;
using Web.Models;

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
            cfg.CreateMap<UserRegistrationModel, InputUserRegistrationDto>(); //model is validated, registration dto isn't.
            //It's just to pass UserService Reg method


            cfg.CreateMap<InputUserRegistrationDto, InputLoginUserDto>();  //why is reg becoming login.
            //when users registrate here they login in RentInHend.

            cfg.CreateMap<OutputAuthTokenDto, Token>();
        }

    }
}
