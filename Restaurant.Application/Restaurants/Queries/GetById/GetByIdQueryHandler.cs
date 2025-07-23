using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.Restaurants.Queries.GetAllResturants;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Restaurants.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, GetResturantDto>
    {
        #region Context
        private readonly ILogger<GetByIdQueryHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetByIdQueryHandler(ILogger<GetByIdQueryHandler> logger,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion
        public async Task<GetResturantDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Get Resturant {request.Id}");
            var restaurant = await unitOfWork.resturantRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
                return null;

            var dto = mapper.Map<GetResturantDto>(restaurant);

            return dto;
        }
    }
}
