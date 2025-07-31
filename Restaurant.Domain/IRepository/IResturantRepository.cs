using Restaurant.Domain.Entities;

namespace Restaurant.Domain.IRepository
{
    public interface IResturantRepository : IGenericRepository<Restaurantt>
    {
        Task<Restaurantt?> GetAllDishesUsingResturantId(int ResturantId);
        Task<bool> SoftDeleteRestaurantAsync(int id);
    }
}
