using DevBoost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Services
{
    public interface IDroneService
    {

        Task<Drone> Criar(Drone  drone);
        Task<IEnumerable<Drone>> ObterTodos();
        Task<Drone> ObterByIdAsync(Guid id);
        Task<Drone> UpdateAsync(Drone  drone);
        Task<Drone> DeleteAsync(Guid id);
        bool DroneExiste(Guid id);
        Task AtualizarStatusDronesAsync();
    }
}
