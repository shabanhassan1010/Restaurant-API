using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.IService;
using Restaurant.Application.Service;

namespace Restaurant.Application.ServicesExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IResturantService, ResturantService>();
            services.AddAutoMapper(typeof(ResturantService).Assembly);
        } 
    }
}
