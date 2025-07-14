using Restaurant.Application.DTOS.Restaurant.Read;

namespace Restaurant.Application.IService
{
    public interface IResturantService
    {
        public Task<IEnumerable<GetResturantDto>> GetAllRestaurants();
    }
}
