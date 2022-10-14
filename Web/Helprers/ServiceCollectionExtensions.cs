using HendInRentApi;

namespace Web.Helprers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRentInHendApiServices(this IServiceCollection services)
        {
            services.AddTransient<AuthInRentInHendApi>();
            services.AddTransient<GenericRepositoryApi>();
        }

    }
}
