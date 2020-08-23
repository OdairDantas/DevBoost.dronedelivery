using System;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        public IPedidoRepository Pedidos { get; }
        public IDroneRepository Drone { get; }
        public IDroneItinerarioRepository DroneItinerario { get; }
        public IEntregaRepository Entrega { get; }

        Task SaveAsync();
    }
}
