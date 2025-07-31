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
        public async Task<bool> SoftDeleteDishAsync(int id)
        {
            // get dish 
            var dish = await context.Dishes.FindAsync(id);
            if (dish == null) return false;

            // set it false if exist
            dish.IsDeleted = true;

            await context.SaveChangesAsync();
            return true;
        }
    }
}
