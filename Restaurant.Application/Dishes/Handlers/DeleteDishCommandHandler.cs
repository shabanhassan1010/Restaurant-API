using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.Commands.DeleteDish;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.Queries.GetAllDishesInResturant;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Dishes.Handlers
{
    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishFromResturantCommand, ApiResponse<GetDishDto>>
    {
        #region Context
        private readonly ILogger<DeleteDishCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<ApiResponse<GetDishDto>> Handle(DeleteDishFromResturantCommand request, CancellationToken cancellationToken)
        {
            //check
            //  Check if the dish exists and belongs to the restaurant
            var dish = await unitOfWork.dishRepository.GetDishByIdAndRestaurantIdAsync(request.DishId , request.RestaurantId);
            if (dish == null)
            {
                return new ApiResponse<GetDishDto>
                {
                    Success = false,
                    Message = $"Dish with ID {request.DishId} not found or Restaurant with Id {request.RestaurantId} is not found  .",
                    Data = null
                };
            }
            //  Delete the dish
            await unitOfWork.dishRepository.SoftDeleteDishAsync(dish.Id);
            await unitOfWork.SaveAsync(); // Ensures changes are persisted

            var dto = mapper.Map<GetDishDto>(dish);
            //  Return success response
            return new ApiResponse<GetDishDto>
            {
                Success = true,
                Message = "Dish deleted successfully.",
                Data = dto
            };
        }
    }
}
