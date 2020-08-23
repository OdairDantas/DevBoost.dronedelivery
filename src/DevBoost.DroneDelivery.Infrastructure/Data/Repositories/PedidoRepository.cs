using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        private readonly IDbContext _context;

        public PedidoRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        

    }
}
