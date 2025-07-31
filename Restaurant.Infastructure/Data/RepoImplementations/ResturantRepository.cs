using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;

namespace Restaurant.Infastructure.Data.RepoImplementations
{
    public class ResturantRepository : GenericRepository<Restaurantt> , IResturantRepository
    {
        private readonly RestaurantDB context;
        public ResturantRepository(RestaurantDB context) : base(context)
        {
            this.context = context;
        }

        public async Task<Restaurantt?> GetAllDishesUsingResturantId(int restaurantId)
        {
            return await context.Restaurantes
                    .Include(r => r.Dishes.Where(d => !d.IsDeleted))
                    .FirstOrDefaultAsync(r => r.Id == restaurantId && !r.IsDeleted);
        }

        public async Task<bool> SoftDeleteRestaurantAsync(int id)
        {
            var restaurant = await context.Restaurantes.FindAsync(id);
            if (restaurant == null) return false;

            restaurant.IsDeleted = true;

            foreach (var dish in context.Dishes.Where(d => d.RestaurantId == id))
            {
                dish.IsDeleted = true;
            }

            await context.SaveChangesAsync();
            return true;
        }
    }
}
