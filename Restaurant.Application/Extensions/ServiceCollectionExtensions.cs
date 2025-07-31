using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.RestaurantService.IService;
using Restaurant.Application.RestaurantService.Service;

namespace Restaurant.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddScoped<IResturantService, ResturantService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly).AddFluentValidationAutoValidation();
        } 
    }
}
