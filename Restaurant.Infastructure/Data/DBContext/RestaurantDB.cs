using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Infastructure.Data.Configurations;
namespace Restaurant.Infastructure.Data.DBContext
{
    public class RestaurantDB : DbContext
    {
        public RestaurantDB(DbContextOptions<RestaurantDB> options):base(options)
        {
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Restaurantt> Restaurantes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ResturantConfiguration());
            modelBuilder.ApplyConfiguration(new DishConfiguration());
        }
    }
}
