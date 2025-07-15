using Restaurant.Application.DTOS.Restaurant.Read;
using Restaurant.Application.DTOS.Restaurant.Write;

namespace Restaurant.Application.IService
{
    public interface IResturantService
    {
        public Task<IEnumerable<GetResturantDto>> GetAllRestaurants();
        public Task<GetResturantDto> GetResturantById(int id);
        public Task<GetResturantDto> GreateResturant(CreateResturanctDto createResturanctDto);
    }
}
