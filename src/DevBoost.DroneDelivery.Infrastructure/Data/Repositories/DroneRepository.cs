using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Repositories
{
    public class DroneRepository : IDroneRepository
    {
        private readonly DroneDeliveryContext _context;
        public DroneRepository(DroneDeliveryContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public async Task DeleteAsync(Drone entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public async Task<IEnumerable<Drone>> GetAllAsync()
        {
            return await _context.Drone.ToListAsync();
        }

        public async Task<IEnumerable<Drone>> GetByAsync(Expression<Func<Drone, bool>> predicate)
        {
            return await Task.Run(() => _context.Drone.Where(predicate));
        }

        public async Task<Drone> GetByIdAsync(Guid id)
        {
            var drone = await Task.Run(() => _context.Drone.Where(p => p.Id == id).FirstOrDefault());

            if (drone.Entregas != null)
            {
                await _context.Entry(drone).Collection(i => i.Entregas).LoadAsync();
            }
            return drone;
        }
        public async Task<Drone> ObterSituacao(Guid id)
        {
            var drone = await _context.Drone.Include(d =>  d.Entregas).Include(i=> i.Itinerarios ).FirstOrDefaultAsync(p => p.Id == id);

            if (drone.Entregas != null)
            {
                await _context.Entry(drone).Collection(i => i.Entregas).LoadAsync();
            }
            return drone;
        }


        public async Task InsertAsync(Drone entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task InsertCollectionAsync(IEnumerable<Drone> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public async Task UpdateAsync(Drone entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.Run(() => _context.Update(entity));
        }

        public async Task UpdateCollectionAsync(IEnumerable<Drone> entities)
        {
            await Task.Run(() => _context.UpdateRange(entities));
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
