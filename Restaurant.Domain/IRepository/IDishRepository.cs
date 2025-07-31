using Restaurant.Domain.Entities;

namespace Restaurant.Domain.IRepository
{
    public interface IDishRepository : IGenericRepository<Dish>
    {
        Task<bool> SoftDeleteDishAsync(int id);
    }
}
