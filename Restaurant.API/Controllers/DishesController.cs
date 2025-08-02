using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Commands.DeleteDish;
using Restaurant.Application.Dishes.Commands.RestoreDish;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Dishes.Queries.GetDishById;
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

        #region Get Dish Using ID
        [HttpGet("{id}")]
        [EndpointSummary("Get Dish Using Id")]
        public async Task<ActionResult<GetDishDto>> GetDishById([FromRoute] int id)
        {
            var res = await mediatR.Send(new GetDishByIdQuery(id));
            if (res == null)
                return NotFound();

            return Ok(res);
        }
        #endregion

        #region Create Dish
        [HttpPost("{RestaurantId}")]
        [EndpointSummary("Create Dish")]
        public async Task<ActionResult<GetDishDto>> CreateDish([FromRoute] int RestaurantId, CreateDishCommand createDishesCommand)
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
        public async Task<ActionResult<GetDishDto>> DeleteDishFromRestaurant([FromRoute] int resturantId , [FromRoute] int dishId)
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
        public async Task<ActionResult<GetDishDto>> RestoreDish([FromRoute] int dishId)
        {
            var result = await mediatR.Send(new RestoreDishCommand(dishId));
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        #endregion
    }
}
