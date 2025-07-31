using MediatR;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.Commands.UpdateResturant
{
    public class UpdateResturantCommand : IRequest<ApiResponse<GetResturantDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public Address? Address { get; set; }
    }
}