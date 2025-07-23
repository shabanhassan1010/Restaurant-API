
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;
using Restaurant.Infastructure.Data.RepoImplementations;

namespace Restaurant.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDB>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IResturantRepository, ResturantRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
