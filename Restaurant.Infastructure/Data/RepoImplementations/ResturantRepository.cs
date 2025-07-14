using Restaurant.Domain.Entities;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;

namespace Restaurant.Infastructure.Data.RepoImplementations
{
    public class ResturantRepository : GenericRepository<Restaurantt> , IResturantRepository
    {
        public ResturantRepository(RestaurantDB context) : base(context)
        {
        }
    }
}
