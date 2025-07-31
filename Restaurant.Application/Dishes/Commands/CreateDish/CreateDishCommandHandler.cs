using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, GetDishDto>
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
        public Task<GetDishDto> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create New Dish {@Dish} with specifce Resturant {@returant}", request , request.RestaurantId);

            // get resturant id
            var GetResturant = unitOfWork.resturantRepository.GetByIdAsync(request.RestaurantId);
            if (GetResturant == null)
                return null;
            

            throw new NotImplementedException();
        }
    }
}
