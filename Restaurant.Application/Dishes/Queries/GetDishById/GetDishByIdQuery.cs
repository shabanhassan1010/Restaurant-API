using MediatR;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;

namespace Restaurant.Application.Dishes.Queries.GetDishById
{
    public class GetDishByIdQuery : IRequest<ApiResponse<GetDishDto>>
    {
        public int DishId { get; set; }
        public GetDishByIdQuery(int dishId)
        {
            DishId = dishId;
        }
    }
}
