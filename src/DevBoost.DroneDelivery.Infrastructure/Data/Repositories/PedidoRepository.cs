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
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DroneDeliveryContext _context;

        public PedidoRepository(DroneDeliveryContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task DeleteAsync(Pedido entity)
        {
            await Task.Run(() => _context.Remove(entity));
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedido.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetByAsync(Expression<Func<Pedido, bool>> predicate)
        {
            var pedidos = await _context.Pedido.Where(predicate).ToListAsync();

            return pedidos;
        }

        public async Task<Pedido> GetByIdAsync(Guid id)
        {
            var pedidos = await _context.Pedido.Where(p => p.Id == id).ToListAsync();
            return pedidos.FirstOrDefault();
        }

        public async Task InsertAsync(Pedido entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task InsertCollectionAsync(IEnumerable<Pedido> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public async Task UpdateAsync(Pedido entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await Task.Run(() => _context.Update(entity));
        }

        public async Task UpdateCollectionAsync(IEnumerable<Pedido> entities)
        {
            await Task.Run(() => _context.UpdateRange(entities));
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
