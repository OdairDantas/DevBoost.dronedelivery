using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class DroneRepository : Repository<Drone>, IDroneRepository
    {
        private readonly IDbContext _context;

        public DroneRepository(IDbContext context) : base(context)
        {
            this._context = context;
        }

        
    }
}
