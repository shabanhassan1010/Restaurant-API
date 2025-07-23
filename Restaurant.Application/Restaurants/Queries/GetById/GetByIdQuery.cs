using MediatR;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;

namespace Restaurant.Application.Restaurants.Queries.GetById
{
    public class GetByIdQuery : IRequest<GetResturantDto?>
    {
        public GetByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
