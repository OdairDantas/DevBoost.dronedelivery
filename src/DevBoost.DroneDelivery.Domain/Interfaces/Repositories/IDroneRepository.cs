using DevBoost.DroneDelivery.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IDroneRepository:IRepository<Drone>
    {
        Task<Drone> ObterSituacao(Guid id);
    }
}
