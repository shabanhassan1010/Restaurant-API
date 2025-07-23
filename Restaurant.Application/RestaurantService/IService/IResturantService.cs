using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Update;

namespace Restaurant.Application.RestaurantService.IService
{
    public interface IResturantService
    {
        Task<GetResturantDto> DeleteResturant(int id);
        Task<ApiResponse<GetResturantDto>> UpdateResturant(UpdateResturanctDto Dto, int id);
    }
}
