using MediatR;
using Restaurant.Application.Dishes.DTOS.Dish;

namespace Restaurant.Application.Restaurants.Queries.GetAllDishesInResturant
{
    public class GetAllDishesWithSpecificResturantQuery : IRequest<IEnumerable<GetDishDto>>
    {

        public GetAllDishesWithSpecificResturantQuery(int resturantId)
        {
             ResturantId = resturantId;
        }
        public int ResturantId { get; set; }
    }
}
