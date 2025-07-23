#region
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Update;
using Restaurant.Application.RestaurantService.IService;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;
#endregion

namespace Restaurant.Application.RestaurantService.Service
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
        public async Task<GetResturantDto> DeleteResturant(int id)
        {
            logger.LogInformation($"Delete Resturant {id}");
            var restaurant = await unitOfWork.resturantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                logger.LogWarning("Restaurant with ID {Id} not found.", id);
                return null;
            }
            await unitOfWork.resturantRepository.DeleteAsync(restaurant);
            await unitOfWork.SaveAsync();

            var dto = mapper.Map<GetResturantDto>(restaurant);
            logger.LogInformation("Successfully deleted restaurant with ID: {Id}", id);
            return dto;
        }
        public async Task<ApiResponse<GetResturantDto>> UpdateResturant(UpdateResturanctDto Dto , int id)
        {
            // must check the id == Resturant which i went to update it
            var restaurant = await unitOfWork.resturantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return new ApiResponse<GetResturantDto>
                {
                    Success = false,
                    Message = $"Restaurant with ID {id} not found.",
                    Data = null
                };
            }
            // Map updated properties to existing entity
            mapper.Map(Dto, restaurant);

            await unitOfWork.resturantRepository.UpdateAsync(restaurant);
            await unitOfWork.SaveAsync();

            // Now i went to return dto for client
            var updatedDto = mapper.Map<GetResturantDto>(restaurant);
            return new ApiResponse<GetResturantDto>
            {
                Success = true,
                Message = "Resrurant Updated Succsuffully",
                Data = updatedDto
            };
        }
    }
}