using AspNetCore.RouteAnalyzer;
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
            var allowRentInHend = "_allowRentInHend";

            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            // Add services to the container.
            builder.Services.AddControllers().AddControllersAsServices();
            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy(allowRentInHend, policy => policy.AllowAnyOrigin());
            });
            builder.Services.AddHttpClient(GEOLOCATION_HTTPCLIENT_NAME, client =>
            {
                var geolocationApiKey = config.GetSection("Tokens")["GeolocationApi"];
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", geolocationApiKey);
            });
            builder.Services
                .AddRouteAnalyzer()
                .AddRentInHendApiServices()
                .AddModelVlidators()
                .AddNativeServices()
                .AddApiRepositrories()
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(allowRentInHend);

            app.UseAuthentication();    
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });



            app.Run();
        }
    }
}