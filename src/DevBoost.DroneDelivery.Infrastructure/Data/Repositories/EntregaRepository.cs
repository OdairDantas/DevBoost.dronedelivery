using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class EntregaRepository : Repository<Entrega>, IEntregaRepository
    {
        private readonly IDbContext _context;

        public EntregaRepository(IDbContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
