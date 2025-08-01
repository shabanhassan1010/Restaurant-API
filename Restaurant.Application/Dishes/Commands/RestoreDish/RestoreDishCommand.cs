using MediatR;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
namespace Restaurant.Application.Dishes.Commands.RestoreDish
{
    public class RestoreDishCommand : IRequest<ApiResponse<GetDishDto>>
    {
        public int DishId { get; set; }

        public RestoreDishCommand(int dishId)
        {
            DishId = dishId;
        }
    }
}
