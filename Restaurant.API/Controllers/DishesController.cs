using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Commands.DeleteDish;
using Restaurant.Application.Dishes.Commands.RestoreDish;
namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class DishesController : ControllerBase
    {
        #region Constructor
        private readonly IMediator mediatR;
        public DishesController(IMediator mediatR)
        {
            this.mediatR = mediatR;
        }
        #endregion

        #region Create Dish
        [HttpPost("{RestaurantId}")]
        [EndpointSummary("Create Dish")]
        public async Task<IActionResult> CreateDish([FromRoute] int RestaurantId, CreateDishCommand createDishesCommand)
        {
            createDishesCommand.RestaurantId = RestaurantId;
            var result = await mediatR.Send(createDishesCommand);
            if(result == null)
                return NotFound();
            return Ok(result);
        }
        #endregion

        #region Soft Delete for Dish From Restaurant
        [HttpDelete("{resturantId}/{dishId}")]
        [EndpointSummary("Delete Dish from Restaurant")]
        public async Task<IActionResult> DeleteDishFromRestaurant([FromRoute] int resturantId , [FromRoute] int dishId)
        {
            var result = await mediatR.Send(new DeleteDishFromResturantCommand(resturantId, dishId));
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        #endregion

        #region Restore soft deleted dish
        [HttpPatch("{dishId}")]
        [EndpointSummary("Restore soft-deleted dish")]
        public async Task<IActionResult> RestoreDish([FromRoute] int dishId)
        {
            var result = await mediatR.Send(new RestoreDishCommand(dishId));
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        #endregion
    }
}
