using Restaurant.Application.DTOS.Restaurant.Read;
using Restaurant.Application.IService;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Service
{
    public class ResturantService : IResturantService
    {
        #region Context
        private readonly IResturantRepository _resturantRepository;
        public ResturantService(IResturantRepository resturantRepository)
        {
            _resturantRepository = resturantRepository;
        }
        #endregion
        public async Task<IEnumerable<GetResturantDto>> GetAllRestaurants()
        {
            var restaurants = await _resturantRepository.GetAllAsync();

            var restaurantDtos = restaurants.Select(r => new GetResturantDto
            {
                Name = r.Name,
                Description = r.Description,
                HasDelivery = r.HasDelivery
            });

            return restaurantDtos;
        }
    }
}
