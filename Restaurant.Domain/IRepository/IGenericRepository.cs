using System.Linq.Expressions;

namespace Restaurant.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IReadOnlySet<T>> FindAsync(Expression<Func<T, bool>> query);
        Task<T> GetByIdAsync(int id);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> CreateAsync(T entity);
    }
}
