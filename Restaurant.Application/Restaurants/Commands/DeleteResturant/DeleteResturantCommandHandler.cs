using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Restaurant.Application.Restaurants.Commands.DeleteResturant
{
    public class DeleteResturantCommandHandler : IRequestHandler<DeleteResturantCommand, ApiResponse<GetResturantDto>>
    {
        #region Constructor
        private readonly ILogger<DeleteResturantCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public DeleteResturantCommandHandler(ILogger<DeleteResturantCommandHandler> logger , 
            IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        public async Task<ApiResponse<GetResturantDto>> Handle(DeleteResturantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Delete Resturant with Id {request.Id}");

            var resturant = await unitOfWork.resturantRepository.GetByIdAsync(request.Id);
            if (resturant == null)
                return new ApiResponse<GetResturantDto>
                {
                    Success = false,
                    Message = $"Resturant with this Id {request.Id} is not exist",
                    Data = null
                };

            await unitOfWork.resturantRepository.SoftDeleteRestaurantAsync(resturant.Id);
            await unitOfWork.SaveAsync();

            var dto = mapper.Map<GetResturantDto>(resturant);

            return new ApiResponse<GetResturantDto>
            {
                Success = true,
                Message = $"Resturant withe Id {request.Id}",
                Data = dto

            };
        }
    }
}