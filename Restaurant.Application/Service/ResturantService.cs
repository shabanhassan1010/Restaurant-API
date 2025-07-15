#region
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.DTOS.Restaurant.Read;
using Restaurant.Application.DTOS.Restaurant.Write;
using Restaurant.Application.IService;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;
#endregion

namespace Restaurant.Application.Service
{
    internal class ResturantService : IResturantService
    {
        #region Context
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ResturantService> logger;
        private readonly IMapper mapper;

        public ResturantService(IUnitOfWork unitOfWork , ILogger<ResturantService> logger , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }
        #endregion
        public async Task<IEnumerable<GetResturantDto>> GetAllRestaurants()
        {
            logger.LogInformation("Get All Restaurants");
            var restaurants = await unitOfWork.resturantRepository.GetAllAsync();

            var dto = mapper.Map<IEnumerable<GetResturantDto>>(restaurants);

            return dto;
        }
        public async Task<GetResturantDto> GetResturantById(int id)
        {
            logger.LogInformation($"Get Resturant {id}");
            var restaurant = await unitOfWork.resturantRepository.GetByIdAsync(id);
            if (restaurant == null)
                return null;

            var dto = mapper.Map<GetResturantDto>(restaurant);

            return dto;
        }
        public async Task<GetResturantDto> GreateResturant(CreateResturanctDto createResturanctDto)
        {
            logger.LogInformation("Create Resturant");

            var mapping = mapper.Map<Restaurantt>(createResturanctDto);

            await unitOfWork.resturantRepository.CreateAsync(mapping);
            await unitOfWork.SaveAsync();

            var result = mapper.Map<GetResturantDto>(mapping);
            return result;
        }
    }
}