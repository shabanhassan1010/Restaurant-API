using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.IService;
using Restaurant.Application.Service;

namespace Restaurant.Application.ServicesExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ResturantService).Assembly;

            services.AddScoped<IResturantService, ResturantService>();
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly).AddFluentValidationAutoValidation();
        } 
    }
}
