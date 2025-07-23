using Restaurant.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTOS.Restaurant.Write
{
    public class CreateResturanctDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public Address? Address { get; set; }
    }
}
