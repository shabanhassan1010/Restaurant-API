using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, ApiResponse<GetDishDto>>
    {
        #region Constructor
        private readonly ILogger<CreateDishCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public async Task<ApiResponse<GetDishDto>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create New Dish {@Dish} with specifce Resturant {@returant}", request , request.RestaurantId);

            // get resturant id
            var GetResturant = await unitOfWork.resturantRepository.GetByIdAsync(request.RestaurantId);
            if (GetResturant == null)
                return new ApiResponse<GetDishDto>
                {
                    Success = false,
                    Message = $"can not create dish in resturant which is not exist in database  ",
                    Data = null
                };

            // Mapping CreateDishCommand before creat it 
            var Mapping = mapper.Map<Dish>(request);

            await unitOfWork.dishRepository.CreateAsync(Mapping);
            await unitOfWork.SaveAsync();

            var dto = mapper.Map<GetDishDto>(Mapping);
            return new ApiResponse<GetDishDto>
            {
                Success = true,
                Message = "Dish Created successfully",
                Data = dto
            };
        }
    }
}
