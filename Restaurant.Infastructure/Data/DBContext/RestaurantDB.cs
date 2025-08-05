using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Infastructure.Data.Configurations;
namespace Restaurant.Infastructure.Data.DBContext
{
    public class RestaurantDB : IdentityDbContext<User>
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
            // change Name of Identity Tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            // Apply your own configurations
            modelBuilder.ApplyConfiguration(new ResturantConfiguration());
            modelBuilder.ApplyConfiguration(new DishConfiguration());      
        }
    }
}
