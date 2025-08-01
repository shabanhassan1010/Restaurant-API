using Restaurant.Domain.Entities;

namespace Restaurant.Domain.IRepository
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        Task SoftDeleteDishAsync(int dishId);
        Task RestoreDishAsync(int dishId);
        Task<Dish> GetDishWhichIsDeletedAsync(int id); // ← NEW
        Task<Dish?> GetDishByIdAndRestaurantIdAsync(int dishId, int restaurantId);
    }
}
