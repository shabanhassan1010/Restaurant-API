
namespace Restaurant.Domain.Entities
{
    public class Dish : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int RestaurantId { get; set; }
        public Restaurantt Restaurant { get; set; } = default!;
    }
}
