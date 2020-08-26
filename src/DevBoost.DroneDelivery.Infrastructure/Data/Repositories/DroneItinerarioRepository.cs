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
    public class DroneItinerarioRepository : IDroneItinerarioRepository
    {
        private DroneDeliveryContext _context;
        public DroneItinerarioRepository(DroneDeliveryContext context)
        {
            _context = context;

        }
        public IUnitOfWork UnitOfWork => _context;


        public async Task DeleteAsync(DroneItinerario entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public async Task<IEnumerable<DroneItinerario>> GetAllAsync()
        {
            return await _context.DroneItinerario.Include(d => d.Drone).ToListAsync();
        }

        public async Task<IEnumerable<DroneItinerario>> GetByAsync(Expression<Func<DroneItinerario, bool>> predicate)
        {
            return await _context.DroneItinerario.Include(D => D.Drone).Where(predicate).ToListAsync();
        }

        public async Task<DroneItinerario> GetByIdAsync(Guid id)
        {
            var itinerario = await Task.Run(() => _context.DroneItinerario.Include(d => d.Drone).Where(p => p.Id == id).FirstOrDefault());

            if (itinerario.Drone != null)
                await _context.Entry(itinerario).Reference(i => i.Drone).LoadAsync();


            return itinerario;
        }

        public async Task InsertAsync(DroneItinerario entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task InsertCollectionAsync(IEnumerable<DroneItinerario> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public async Task UpdateAsync(DroneItinerario entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.Run(() => _context.Update(entity));
        }

        public async Task UpdateCollectionAsync(IEnumerable<DroneItinerario> entities)
        {
            await Task.Run(() => _context.UpdateRange(entities));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
