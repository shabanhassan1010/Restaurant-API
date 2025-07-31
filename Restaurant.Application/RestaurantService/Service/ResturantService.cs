#region
using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Application.CustomeResponse;
using Restaurant.Application.Restaurants.DTOS.Restaurant.Read;
using Restaurant.Application.RestaurantService.IService;
using Restaurant.Domain.IRepository;
#endregion

namespace Restaurant.Application.RestaurantService.Service
{
    internal class ResturantService : IResturantService
    {
        #region Context
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<ResturantService> logger;
        private readonly IMapper mapper;

        public ResturantService(IUnitOfWork unitOfWork , ILogger<ResturantService> logger , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.mapper = mapper;
        }
        #endregion
       
    }
}