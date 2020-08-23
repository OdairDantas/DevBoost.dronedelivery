using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Contexts
{
    public interface IDbContext:IDisposable
    {

        DbSet<T> Set<T>() where T : class;

        Task<int> SaveChangesAsync();
    }
}
