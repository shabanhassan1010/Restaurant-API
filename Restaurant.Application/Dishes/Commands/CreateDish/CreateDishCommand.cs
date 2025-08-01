using MediatR;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;

namespace Restaurant.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommand : IRequest<ApiResponse<GetDishDto>>
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
    }
}
