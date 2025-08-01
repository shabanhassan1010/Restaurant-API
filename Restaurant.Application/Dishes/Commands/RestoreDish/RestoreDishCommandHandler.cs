using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Domain.IRepository;
namespace Restaurant.Application.Dishes.Commands.RestoreDish
{
    public class RestoreDishCommandHandler : IRequestHandler<RestoreDishCommand, ApiResponse<GetDishDto>>
    {
        private readonly ILogger<RestoreDishCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public RestoreDishCommandHandler(ILogger<RestoreDishCommandHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<GetDishDto>> Handle(RestoreDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await unitOfWork.dishRepository.GetDishWhichIsDeletedAsync(request.DishId);

            if (dish == null || dish.IsDeleted == false)
            {
                return new ApiResponse<GetDishDto>
                {
                    Success = false,
                    Message = "Dish not found or is already active.",
                    Data = null
                };
            }

            await unitOfWork.dishRepository.RestoreDishAsync(dish.Id);
            await unitOfWork.SaveAsync();

            var dto = mapper.Map<GetDishDto>(dish);
            return new ApiResponse<GetDishDto>
            {
                Success = true,
                Message = "Dish restored successfully.",
                Data = dto
            };
        }
    }

}
