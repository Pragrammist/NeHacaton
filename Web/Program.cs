using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using Web.Helprers;
using static Web.Constants.GeolocationConstants;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllersWithViews().AddControllersAsServices();
            builder.Services.AddHttpClient(GEOLOCATION_HTTPCLIENT_NAME, client =>
            {
                var geolocationApiKey = config.GetSection("Tokens")["GeolocationApi"];
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", geolocationApiKey);
            });
            builder.Services
                .AddRentInHendApiServices()
                .AddModelVlidators()
                .AddNativeServices()
                .AddDbContexts(config)
                .ConfigAutoMapper()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    options.LoginPath = new PathString("/User/Login");
                    options.LogoutPath = new PathString("/User/Logout");
                });

           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();    
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Login}/{id?}");

            app.Run();
        }
    }
}