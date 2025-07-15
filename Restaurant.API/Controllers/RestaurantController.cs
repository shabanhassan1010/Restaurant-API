#region
using Microsoft.AspNetCore.Mvc;
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
                return NotFound($"The Restaurant With Id {id} is not found");

            return Ok(restaurants);
        }
        #endregion

        #region  Greate Resturant
        [HttpPost]
        [EndpointSummary("Create Restaurant")]
        public async Task<IActionResult> CreateRestaurant(CreateResturanctDto createResturanctDto)
        {
            var resturant = await _service.GreateResturant(createResturanctDto);
            if (resturant == null)
                return BadRequest();
            return CreatedAtAction(nameof(GetById) ,  new { id = resturant.Id }, resturant);
        }
        #endregion

        #region 

        #endregion

        #region  

        #endregion

        #region  

        #endregion
    }
}
