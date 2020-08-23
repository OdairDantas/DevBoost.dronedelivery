using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdAsync(int id);
        Task InsertCollectionAsync(IEnumerable<T> entity);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateCollectionAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
    }
}
