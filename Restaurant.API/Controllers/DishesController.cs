using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
namespace Restaurant.API.Controllers
{
    [Route("api/Restaurant/{RestaurantId}/[controller]")]
    [ApiController] 
    public class DishesController : ControllerBase
    {
        //public async Task<IActionResult> CreateDish([FromRoute] int RestaurantId , CreateDishCommand createDishesCommand)
        //{

        //}
    }
}
