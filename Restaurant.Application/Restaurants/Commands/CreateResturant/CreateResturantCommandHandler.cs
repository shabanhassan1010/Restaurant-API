using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Restaurants.Commands.CreateResturant
{
    public class CreateResturantCommandHandler : IRequestHandler<CreateResturantCommand, GetResturantDto>
    {
        private readonly ILogger<CreateResturantCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateResturantCommandHandler(ILogger<CreateResturantCommandHandler> logger , 
            IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<GetResturantDto> Handle(CreateResturantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create Resturant");

            var mapping = mapper.Map<Restaurantt>(request);

            await unitOfWork.resturantRepository.CreateAsync(mapping);
            await unitOfWork.SaveAsync();

            var result = mapper.Map<GetResturantDto>(mapping);
            return result;
        }
    }
}
