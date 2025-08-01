using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;

namespace Restaurant.Infastructure.Data.RepoImplementations
{
    public class DishRepository : GenericRepository<Dish>, IDishRepository
    {
        #region Context
        private readonly RestaurantDB context;
        public DishRepository(RestaurantDB context) : base(context)
        {
            this.context = context;
        }
        #endregion
        public async Task SoftDeleteDishAsync(int dishId)
        {
            var dish = await context.Dishes.FindAsync(dishId);
            if (dish != null)
            {
                dish.IsDeleted = true;
                context.Dishes.Update(dish);
            }
        }
        public async Task RestoreDishAsync(int dishId)
        {
            var dish = await context.Dishes.FindAsync(dishId);
            if (dish != null && dish.IsDeleted)
            {
                dish.IsDeleted = false;
                context.Dishes.Update(dish);
            }
        }
        public async Task<Dish?> GetDishByIdAndRestaurantIdAsync(int dishId, int restaurantId)
        {
            return await context.Dishes
                .Where(d => d.Id == dishId && d.RestaurantId == restaurantId && !d.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<Dish> GetDishWhichIsDeletedAsync(int id)   // get all Dishes which i deleted it using soft delete
        {
            return await context.Dishes
                .IgnoreQueryFilters().FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}