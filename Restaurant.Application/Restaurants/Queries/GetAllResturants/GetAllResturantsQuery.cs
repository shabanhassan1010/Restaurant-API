using MediatR;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;

namespace Restaurant.Application.Restaurants.Queries.GetAllResturants
{
    public class GetAllResturantsQuery : IRequest<IEnumerable<GetResturantDto>>
    {
    }
}
