using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.Commands.UpdateResturant;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Domain.IRepository;

namespace Restaurant.Application.Restaurants.Handlers
{
    public class UpdateResturantCommandHandler : IRequestHandler<UpdateResturantCommand, ApiResponse<GetResturantDto>>
    {
        #region Constructor
        private readonly ILogger<UpdateResturantCommandHandler> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateResturantCommandHandler(ILogger<UpdateResturantCommandHandler> logger ,
            IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        public async Task<ApiResponse<GetResturantDto>> Handle(UpdateResturantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Update Resturant with id {request.Id}");
            var GetResturant = await unitOfWork.resturantRepository.GetByIdAsync(request.Id);
            if (GetResturant == null)
                return new ApiResponse<GetResturantDto>
                {
                    Success = false,
                    Message = $"Restaurant with ID {request.Id} not found. Update failed.",
                    Data = null
                };

            mapper.Map(request , GetResturant);
            await unitOfWork.resturantRepository.UpdateAsync(GetResturant);
            await unitOfWork.SaveAsync();

            var dto = mapper.Map<GetResturantDto>(GetResturant);
            return new ApiResponse<GetResturantDto>
            {
                Success = true,
                Message = $"The Resturant with id {request.Id} is Updated successfully",
                Data = dto
            };
        }
    }
}