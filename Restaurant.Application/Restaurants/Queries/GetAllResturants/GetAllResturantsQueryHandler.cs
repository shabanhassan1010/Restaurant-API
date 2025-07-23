using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.Commands.CreateResturant;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Restaurants.Queries.GetAllResturants
{
    public class GetAllResturantsQueryHandler : IRequestHandler<GetAllResturantsQuery, IEnumerable<GetResturantDto>>
    {
        #region Context
        private readonly ILogger<GetAllResturantsQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetAllResturantsQueryHandler(ILogger<GetAllResturantsQueryHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public async Task<IEnumerable<GetResturantDto>> Handle(GetAllResturantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Get All Restaurants" , request);
            var restaurants = await unitOfWork.resturantRepository.GetAllAsync();

            var dto = mapper.Map<IEnumerable<GetResturantDto>>(restaurants);

            return dto;
        }
    }
}
