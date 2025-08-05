using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Users;

namespace Restaurant.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly).AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();  // im use this beacuse i inject it in UserContext
        } 
    }
}
