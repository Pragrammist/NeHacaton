using Microsoft.AspNetCore.Mvc;

namespace Web.Helprers
{
    public static class ControllerExtensions
    {
        public static string? GetCityFromHttpContext(this ControllerBase controller)
        {
            var context = controller.HttpContext;
            string? city;
            context.Request.Cookies.TryGetValue("city", out city);
            city = city ?? context.Items["city"]?.ToString();
            return city;
        }
    }
}
