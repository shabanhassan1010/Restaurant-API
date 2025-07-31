namespace Restaurant.Application.Dishes.DTOS.Dish
{
    public class GetDishDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string? RestaurantName { get; set; }
    }
}
