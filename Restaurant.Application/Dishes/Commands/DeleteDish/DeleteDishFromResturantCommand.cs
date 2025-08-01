using MediatR;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;

namespace Restaurant.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishFromResturantCommand : IRequest<ApiResponse<GetDishDto>>
    {
        public int RestaurantId { get; set; }
        public int DishId { get; set; }

        public DeleteDishFromResturantCommand(int restaurantId , int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
    }
}
