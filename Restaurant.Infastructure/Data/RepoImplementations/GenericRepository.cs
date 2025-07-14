using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.IRepository;
using Restaurant.Infastructure.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infastructure.Data.RepoImplementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region DB Context
        private readonly RestaurantDB context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(RestaurantDB context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }
        #endregion

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlySet<T>> FindAsync(Expression<Func<T, bool>> query)
        {
            var result = await _dbSet.AsNoTracking().Where(query).ToListAsync();
            return result.ToHashSet();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
