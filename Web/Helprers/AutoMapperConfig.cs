

using AutoMapper;
using DataBase.Entities;
using HendInRentApi;
using Web.Dtos;
using Web.Dtos.Sales.Inventory;
using Web.Cryptographer;
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
            cfg.CreateMap<UserRegistrationModel, InputUserRegistrationDto>(); 


            cfg.CreateMap<InputUserRegistrationDto, InputHIRALoginUserDto>();  
            //почему регистрация превращается, в логин. Первый объект для регистрации у нас
            //второй для того чтобы авторизоваться в Rent In Hand

            
            cfg.CreateMap<InputUserRegistrationDto, User>().ForMember(t => t.Password, cfg => cfg.Ignore()); 
            //т.к. пароль хешируется, то он игнорирутеся здесь
            

            cfg.CreateMap<UserLoginModel, InputLoginUserDto>();

            cfg.CreateMap<User, OutputUserDto>();

            cfg.CreateMap<InventorySearchModel, InputSearchInventoryDto>();
        }        
    }
}
