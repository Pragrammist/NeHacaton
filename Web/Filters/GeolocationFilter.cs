using Microsoft.AspNetCore.Mvc.Filters;
using Web.Geolocation;

namespace Web.Filters
{
    public class GeolocationFilter : IAsyncResourceFilter
    {
        GeolocationRepository _geoRepo;
        public GeolocationFilter(GeolocationRepository geoRepo)
        {
            _geoRepo = geoRepo;
        }
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            if (HasCity(httpContext))
            {
                await next();
                return;
            }

            string? lat;
            string? lon;
            if (httpContext.Request.Cookies.TryGetValue("lat", out lat) && httpContext.Request.Cookies.TryGetValue("lon", out lon))
            {
                InsertDotes(ref lat, ref lon);
                await TryWriteCityToCookies(httpContext, lat, lon);
            }

            await next();
        }
        void InsertDotes(ref string lat, ref string lon)
        {
            lat = lat.Replace('.', ',');
            lon = lon.Replace('.', ',');
        }
        async Task TryWriteCityToCookies(HttpContext context, string strLat, string strLon)
        {
            double lat;
            double lon;
            if (double.TryParse(strLon, out lon) && double.TryParse(strLat, out lat))
            {
                await TryWriteCityToCookies(context, lat, lon);
            }
        }
        async Task TryWriteCityToCookies(HttpContext context, double lat, double lon)
        {
            try
            {
                var city = (await _geoRepo.GetUserLocationByLatLon(lat, lon)).City;
                context.Response.Cookies.Append("city", city);
                context.Items["city"] = city;
            }
            finally
            {

            }
        }

        bool HasCity(HttpContext context)
        {
            return context.Request.Cookies.ContainsKey("city");
        }
    }
}
