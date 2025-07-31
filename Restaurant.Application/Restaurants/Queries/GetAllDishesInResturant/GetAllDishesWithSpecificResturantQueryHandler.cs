using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Dishes.DTOS.Dish;
using Restaurant.Application.Restaurants.Queries.GetAllResturants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Restaurants.Queries.GetAllDishesInResturant
{
    public class GetAllDishesWithSpecificResturantQueryHandler : IRequestHandler<GetAllDishesWithSpecificResturantQuery, IEnumerable<GetDishDto>>
    {
        #region Context
        private readonly ILogger<GetAllDishesWithSpecificResturantQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetAllDishesWithSpecificResturantQueryHandler(ILogger<GetAllDishesWithSpecificResturantQueryHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public async Task<IEnumerable<GetDishDto>> Handle(GetAllDishesWithSpecificResturantQuery request, CancellationToken cancellationToken)
        {
            //Get Resturant ID
            var GetAllDishes = await unitOfWork.resturantRepository.GetAllDishesUsingResturantId(request.ResturantId);
            if (GetAllDishes == null || GetAllDishes.Dishes == null)
                return Enumerable.Empty<GetDishDto>();

            return mapper.Map<IEnumerable<GetDishDto>>(GetAllDishes.Dishes); ;
        }
    }
}
