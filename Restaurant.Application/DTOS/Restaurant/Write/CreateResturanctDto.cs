using Restaurant.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.DTOS.Restaurant.Write
{
    public class CreateResturanctDto
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        [StringLength(100)]
        [Required]
        public string Category { get; set; }

        public bool HasDelivery { get; set; }
        [EmailAddress]
        [Required]
        public string? ContactEmail { get; set; }
        [Phone]
        public string? ContactNumber { get; set; }

        public Address? Address { get; set; }
    }
}
