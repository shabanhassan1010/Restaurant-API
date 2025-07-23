using Restaurant.Application.CustomeResponse;
using Restaurant.Application.DTOS.Restaurant.Read;
using Restaurant.Application.DTOS.Restaurant.Update;
using Restaurant.Application.DTOS.Restaurant.Write;

namespace Restaurant.Application.IService
{
    public interface IResturantService
    {
        Task<IEnumerable<GetResturantDto>> GetAllRestaurants();
        Task<GetResturantDto> GetResturantById(int id);
        Task<GetResturantDto> DeleteResturant(int id);
        Task<GetResturantDto> CreateResturant(CreateResturanctDto Dto);
        Task<ApiResponse<GetResturantDto>> UpdateResturant(UpdateResturanctDto Dto, int id);
    }
}
