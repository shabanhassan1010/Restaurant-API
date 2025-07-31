using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infastructure.Data.Configurations
{
    public class ResturantConfiguration : IEntityTypeConfiguration<Restaurantt>
    {
        public void Configure(EntityTypeBuilder<Restaurantt> builder)
        {
            builder.OwnsOne(r => r.Address);

            builder.HasMany(r => r.Dishes)
                .WithOne(d => d.Restaurant)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
