
namespace Restaurant.Domain.IRepository
{
    public interface IUnitOfWork
    {
        public IResturantRepository  resturantRepository { get; set; }
        Task<bool> SaveAsync();
    }
}
