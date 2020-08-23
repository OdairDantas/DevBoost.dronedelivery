using DevBoost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Domain.Interfaces.Services
{
    public interface IPedidoService
    {
        Task<Pedido> Criar(Pedido pedido);
        Task<IEnumerable<Pedido>> ObterTodos();
        Task<Pedido> ObterByIdAsync(Guid id);
        Task<Pedido> UpdateAsync(Pedido pedido);
        Task<Pedido> DeleteAsync(Guid id);
        bool PedidoExiste(Guid id);
    }
}
