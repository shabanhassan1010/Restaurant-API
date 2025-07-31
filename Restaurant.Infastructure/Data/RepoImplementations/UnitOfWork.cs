using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;

namespace Restaurant.Infastructure.Data.RepoImplementations
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Context
        private readonly RestaurantDB _context;
        public UnitOfWork(RestaurantDB context)
        {
            _context = context;
        }
        #endregion

        #region resturant Repository
        private IResturantRepository _resturantRepository;
        public IResturantRepository resturantRepository 
        {
            get
            {
                if( _resturantRepository == null )
                    _resturantRepository = new ResturantRepository( _context );
                return _resturantRepository;
            }
            set => _resturantRepository = value;
        }
        #endregion

        #region Dish Repository
        private IDishRepository _dishRepository;
        public IDishRepository dishRepository
        {
            get
            {
                if (_dishRepository == null)
                    _dishRepository = new DishRepository(_context);
                return _dishRepository;
            }
            set => _dishRepository = value;
        }
        #endregion
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0; // return true -> if at least one row was modified on Database  
        }
    }
}
