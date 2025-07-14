#region
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.IService;
#endregion

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        #region
        private readonly IResturantService _service;
        public RestaurantController(IResturantService resturantService)
        {
            _service = resturantService;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await _service.GetAllRestaurants();
            return Ok(restaurants);
        }
    }
}
