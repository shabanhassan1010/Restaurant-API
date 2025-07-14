namespace Restaurant.Domain.Entities
{
    public class Restaurantt : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        public virtual Address? Address { get; set; }
        public virtual List<Dish> Dishes { get; set; } = new();
    }
}
