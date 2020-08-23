using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IDbContext context,IEntregaRepository entrega, IPedidoRepository pedidos, IDroneRepository drones, IDroneItinerarioRepository droneItinerario)
        {
            _Context = context;
            Pedidos = pedidos;
            Drone = drones;
            DroneItinerario = droneItinerario;
            Entrega = entrega;
        }
        public IDbContext _Context;
        public IPedidoRepository Pedidos { get; private set; }

        public IDroneRepository Drone { get; private set; }

        public IDroneItinerarioRepository DroneItinerario { get; private set; }
        public IEntregaRepository  Entrega { get; private set; }

        public void Dispose()
        {
            _Context?.Dispose();
        }

        public async Task SaveAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
