#region
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Update;
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
        private readonly IResturantService _service;
        private readonly IMediator mediator;

        public RestaurantController(IResturantService resturantService , IMediator mediator)
        {
            _service = resturantService;
            this.mediator = mediator;
        }
        #endregion

        #region Get All 
        [HttpGet]
        [EndpointSummary("Get all Restaurants")]
        public async Task<ActionResult<IEnumerable<GetResturantDto>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllResturantsQuery());
            return Ok(restaurants);
        }
        #endregion

        #region Get Restaurant By Id
        [HttpGet("{id}")]
        [EndpointSummary("Get Restaurant By Id")]
        public async Task<ActionResult<GetResturantDto>> GetById([FromRoute] int id)
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
            var resturant = await _service.DeleteResturant(id);
            if (resturant == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false, 
                    Message = $"Restaurant with ID {id} not found.",  
                    Data = null
                });
            }
            return Ok(new ApiResponse<GetResturantDto>
            {
                Success = true, 
                Message = "Restaurant deleted successfully.", 
                Data = resturant
            });
        }
        #endregion

        #region Update Resturant
        [HttpPut("/{id}")]
        [EndpointSummary("Update Restaurant")]
        public async Task<ActionResult<GetResturantDto>> CreateRestaurant([FromBody]UpdateResturanctDto Dto ,[FromRoute] int id)
        {
            var resturant = await _service.UpdateResturant(Dto , id);
            if (!resturant.Success)
                return NotFound(resturant);

            return Ok(resturant);
        }
        #endregion
    }
}
