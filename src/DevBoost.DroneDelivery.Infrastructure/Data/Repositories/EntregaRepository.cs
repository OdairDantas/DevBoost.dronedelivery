using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
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
    public class EntregaRepository : IEntregaRepository
    {
        private readonly DroneDeliveryContext _context;

        public EntregaRepository(DroneDeliveryContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task DeleteAsync(Entrega entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public async Task<IEnumerable<Entrega>> GetAllAsync()
        {
            return await _context.Entrega.Include(e => new { e.Pedido, e.Drone }).ToListAsync();
        }

        public async Task<IEnumerable<Entrega>> GetByAsync(Expression<Func<Entrega, bool>> predicate)
        {
            return await Task.Run(() => _context.Entrega.Include(e => new { e.Pedido, e.Drone }).Where(predicate).ToListAsync());
        }

        public async Task<Entrega> GetByIdAsync(Guid id)
        {
            var entrega = await Task.Run(() => _context.Entrega.Include(e => new { e.Pedido, e.Drone }).Where(p => p.Id == id).FirstOrDefault());
            if (entrega.Pedido != null)
                await _context.Entry(entrega).Reference(i => i.Pedido).LoadAsync();
            if (entrega.Drone != null)
                await _context.Entry(entrega).Reference(i => i.Drone).LoadAsync();


            return entrega;
        }

        public async Task InsertAsync(Entrega entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task InsertCollectionAsync(IEnumerable<Entrega> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public async Task UpdateAsync(Entrega entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await Task.Run(() => _context.Update(entity));
        }

        public async Task UpdateCollectionAsync(IEnumerable<Entrega> entities)
        {
            await Task.Run(() => _context.UpdateRange(entities));
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
