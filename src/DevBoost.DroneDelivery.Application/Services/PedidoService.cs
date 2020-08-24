using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly IUnitOfWork _unitOfWork;
        private IMediatrHandler _bus;
        public PedidoService(IUnitOfWork unitOfWork, IMediatrHandler bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        public async Task<Pedido> Criar(Pedido pedido)
        {
            await _unitOfWork.Pedidos.InsertAsync(pedido);
            await _unitOfWork.SaveAsync();
            await _bus.PublicarEvento(new SolicitarEntregaEvent(pedido.Id));
            

            return pedido;
        }
        public async Task<IEnumerable<Pedido>> ObterTodos()
        {
            return await Task.FromResult(_unitOfWork.Pedidos.GetAllAsync().Result);
        }
        public async Task<Pedido> ObterByIdAsync(Guid id)
        {
            return await _unitOfWork.Pedidos.GetByIdAsync(id);
        }
        public async Task<Pedido> UpdateAsync(Pedido pedido)
        {
            if (!PedidoExiste(pedido.Id))
                return null;

            await _unitOfWork.Pedidos.UpdateAsync(pedido);
            await _unitOfWork.SaveAsync();

            return pedido;

        }

        public async Task<Pedido> DeleteAsync(Guid id)
        {
            var pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
            if (pedido == null)
            {
                return null;
            }

            await _unitOfWork.Pedidos.DeleteAsync(pedido);
            await _unitOfWork.SaveAsync();

            return pedido;

        }
        public bool PedidoExiste(Guid id)
        {
            var pedido = _unitOfWork.Pedidos.GetByIdAsync(id);

            if (pedido == null)
                return false;

            return true;
        }
    }
}
