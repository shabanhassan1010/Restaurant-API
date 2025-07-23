#region
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.DTOS.Restaurant.Read;
using Restaurant.Application.DTOS.Restaurant.Update;
using Restaurant.Application.DTOS.Restaurant.Write;
using Restaurant.Application.IService;
#endregion

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        #region resturantService
        private readonly IResturantService _service;
        public RestaurantController(IResturantService resturantService)
        {
            _service = resturantService;
        }
        #endregion

        #region Get All 
        [HttpGet]
        [EndpointSummary("Get all Restaurants")]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _service.GetAllRestaurants();
            return Ok(restaurants);
        }
        #endregion

        #region Get Restaurant By Id
        [HttpGet("{id}")]
        [EndpointSummary("Get Restaurant By Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var restaurants = await _service.GetResturantById(id);
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
        public async Task<IActionResult> CreateRestaurant(CreateResturanctDto createResturanctDto)
        {
            var resturant = await _service.CreateResturant(createResturanctDto);
            if (resturant == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById) ,  new { id = resturant.Id }, resturant);
        }
        #endregion

        #region Delete Resturant
        [HttpDelete("/{id}")]
        [EndpointSummary("Delete Restaurant")]
        public async Task<IActionResult> DeleteRestaurant(int id)
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
        public async Task<IActionResult> CreateRestaurant(UpdateResturanctDto Dto , int id)
        {
            var resturant = await _service.UpdateResturant(Dto , id);
            if (!resturant.Success)
                return NotFound(resturant);

            return Ok(resturant);
        }
        #endregion
    }
}
