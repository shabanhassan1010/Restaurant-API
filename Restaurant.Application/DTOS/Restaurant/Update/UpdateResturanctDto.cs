using Restaurant.Domain.Entities;

namespace Restaurant.Application.DTOS.Restaurant.Update
{
    public class UpdateResturanctDto
    {
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public Address? Address { get; set; }
    }
}
