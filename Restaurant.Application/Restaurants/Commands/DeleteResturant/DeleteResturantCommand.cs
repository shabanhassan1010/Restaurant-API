using MediatR;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.Commands.DeleteResturant
{
    public class DeleteResturantCommand : IRequest<ApiResponse<GetResturantDto>>
    {
        public int Id { get; set; }
        public DeleteResturantCommand(int id)
        {
            Id = id;
        }
    }
}