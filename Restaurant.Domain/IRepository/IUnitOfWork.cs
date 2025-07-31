
namespace Restaurant.Domain.IRepository
{
    public interface IUnitOfWork
    {
        public IResturantRepository  resturantRepository { get; set; }
        public IDishRepository  dishRepository { get; set; }
        Task<bool> SaveAsync();
    }
}
