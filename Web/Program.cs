using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using Web.Helprers;
using static Web.Constants.GeolocationConstants;
using Web.Filters;


namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            builder.Services.AddCors(t => t.AddDefaultPolicy(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            builder.Services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add<GeolocationFilter>();
            });
            

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
                .AddApiRepositrories()
                .AddDbContexts(config)
                .ConfigAutoMapper()
                .AddCachers()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    options.LoginPath = new PathString("/User/Login");
                    options.LogoutPath = new PathString("/User/Logout");
                });

            builder.Services.AddMemoryCache();

            var app = builder.Build();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
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