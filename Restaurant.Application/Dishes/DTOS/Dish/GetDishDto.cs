namespace Restaurant.Application.Dishes.DTOS.Dish
{
    public class GetDishDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsDeleted { get; set; } = false;
        public decimal Price { get; set; }
        public string? RestaurantName { get; set; }
    }
}
