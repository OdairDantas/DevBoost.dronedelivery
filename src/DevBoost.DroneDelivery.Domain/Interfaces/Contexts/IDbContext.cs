using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Contexts
{
    public interface IDbContext : IUnitOfWork,IDisposable
    {
        DbSet<T> Set<T>() where T : class;

    }
}
