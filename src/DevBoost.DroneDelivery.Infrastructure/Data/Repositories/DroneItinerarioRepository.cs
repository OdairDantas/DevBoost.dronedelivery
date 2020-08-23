using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class DroneItinerarioRepository : Repository<DroneItinerario>, IDroneItinerarioRepository
    {
        private readonly IDbContext _context;

        public DroneItinerarioRepository(IDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
