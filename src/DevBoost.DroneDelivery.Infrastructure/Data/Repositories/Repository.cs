using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        internal DbSet<T> _dbset;

        public Repository(IDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbset.FindAsync(id);
        }
       
        public async Task InsertAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task InsertCollectionAsync(IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }

        public Task UpdateAsync(T entity)
        {
            return Task.Run(() => _dbset.Update(entity));
        }

        public Task UpdateCollectionAsync(IEnumerable<T> entities)
        {
            return Task.Run(() => _dbset.UpdateRange(entities));
        }

        public Task DeleteAsync(T entity)
        {
            return Task.Run(()=> _dbset.Remove(entity));
        }

        
    }
}
