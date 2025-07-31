#region
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Application.Restaurants.Commands.DeleteResturant;
using Restaurant.Application.Restaurants.Commands.UpdateResturant;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.Queries.GetAllDishesInResturant;
using Restaurant.Application.Restaurants.Queries.GetAllResturants;
using Restaurant.Application.Restaurants.Queries.GetById;
using Restaurant.Application.RestaurantService.IService;
using Restaurant.Application.Validators;
#endregion

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        #region resturantService
        private readonly IMediator mediator;

        public RestaurantController( IMediator mediator)
        {
            this.mediator = mediator;
        }
        #endregion

        #region Get All Restaurants
        [HttpGet]
        [EndpointSummary("Get all Restaurants")]
        public async Task<ActionResult<IEnumerable<GetResturantDto>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllResturantsQuery());
            return Ok(restaurants);
        }
        #endregion

        #region Get All Dishes With Specific Resturant Id
        [HttpGet("GetAllDishes/{restaurantId}")]
        [EndpointSummary("Get All Dishes With Specific restaurant Id")]
        public async Task<ActionResult<IEnumerable<GetDishDto>>> GetAllDishes(int restaurantId)
        {
            var restaurants = await mediator.Send(new GetAllDishesWithSpecificResturantQuery(restaurantId));
            return Ok(restaurants);
        }

        #endregion

        #region Get Restaurant By Id
        [HttpGet("GetById/{id}")]
        [EndpointSummary("Get Restaurant By Id")]
        public async Task<ActionResult<GetResturantDto>> GetById(int id)
        {
            var restaurants = await mediator.Send(new GetByIdQuery(id));

            if(restaurants == null)
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = $"Restaurant with ID {id} not found.",
                    Data = null
                });

            return Ok(new ApiResponse<GetResturantDto>
            {
                Success = true,
                Message = $"Restaurant with ID {id} Is Exist.",
                Data = restaurants
            });
        }
        #endregion

        #region  Create Resturant
        [HttpPost]
        [EndpointSummary("Create Restaurant")]
        public async Task<ActionResult<GetResturantDto>> CreateRestaurant([FromBody] CreateResturantCommand createResturanctDto)
        {
                var resturant = await mediator.Send(createResturanctDto);
                if (resturant == null)
                    return BadRequest();
                return CreatedAtAction(nameof(GetById), new { id = resturant.Id }, resturant);
            
        }
        #endregion

        #region Delete Resturant
        [HttpDelete("/{id}")]
        [EndpointSummary("Delete Restaurant")]
        public async Task<ActionResult<GetResturantDto>> DeleteRestaurant([FromRoute]int id)
        {
            var resturant = await mediator.Send(new DeleteResturantCommand(id));
            if (!resturant.Success)
            {
                return NotFound(resturant);
            }

            return Ok(resturant);
        }
        #endregion

        #region Update Resturant
        [HttpPut("{id}")]
        [EndpointSummary("Update Restaurant")]
        public async Task<ActionResult<GetResturantDto>> UpdateRestaurant([FromBody]UpdateResturantCommand updateResturantCommand ,[FromRoute] int id)
        {
            updateResturantCommand.Id = id;
            var IsUpdated = await mediator.Send(updateResturantCommand);
            if (IsUpdated == null)
                return NotFound();

            return Ok(IsUpdated);
        }
        #endregion
    }
}
