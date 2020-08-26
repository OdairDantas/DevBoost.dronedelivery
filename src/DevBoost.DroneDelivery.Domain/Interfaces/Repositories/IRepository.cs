using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IRepository<T>: IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> predicate);
        Task InsertCollectionAsync(IEnumerable<T> entity);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateCollectionAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        IUnitOfWork UnitOfWork { get; }
    }
}
