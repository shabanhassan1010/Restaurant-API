using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Dishes.Queries.GetDishById;
using Restaurant.Application.Restaurants.Queries.GetById;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Dishes.Handlers
{
    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, ApiResponse<GetDishDto>>
    {
        #region Context
        private readonly ILogger<GetDishByIdQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetDishByIdQueryHandler(ILogger<GetDishByIdQueryHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public  async Task<ApiResponse<GetDishDto>> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            // get dish Id
            var DishId = await unitOfWork.dishRepository.GetByIdAsync(request.DishId);
            if (DishId == null)
                return new ApiResponse<GetDishDto>
                {
                    Success = false,
                    Message = $"Dish with Id {request.DishId} is not Exist",
                    Data = null
                };
            var dto = mapper.Map<GetDishDto>(DishId);
            return new ApiResponse<GetDishDto>
            {
                Success = true,
                Message = $"Dish with Id {request.DishId} is Exist",
                Data = dto
            };
        }
    }
}
