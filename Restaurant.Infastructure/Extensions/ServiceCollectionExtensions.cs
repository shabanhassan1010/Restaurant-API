using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;
using Restaurant.Infastructure.Data.RepoImplementations;

namespace Restaurant.Infastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<RestaurantDB>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));  // Database setup
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<User, IdentityRole>()     // Identity 
                .AddEntityFrameworkStores<RestaurantDB>().AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
        }
    }
}
