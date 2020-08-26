using DevBoost.DroneDelivery.Application.Queries.ViewModel;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidos()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            var pedidosView = new List<PedidoViewModel>();

            foreach (var pedido in pedidos)
            {
                pedidosView.Add(new PedidoViewModel
                {
                    Id = pedido.Id,
                    DataCadastro = pedido.DataHora,
                    Peso = pedido.Peso,
                    Latitude = pedido.Latitude,
                    Longitude = pedido.Longitude,
                    Status = pedido.Status
                });
            }

            return pedidosView;
        }
        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosAguardandoEntregador()
        {
            var pedidos = await _pedidoRepository.GetAllAsync();
            var pedidosAguardando = pedidos.Where(x => x.Status == EStatusPedido.AguardandoEntregador);
            var pedidosView = new List<PedidoViewModel>();

            foreach (var pedido in pedidosAguardando)
            {
                pedidosView.Add(new PedidoViewModel
                {
                    Id = pedido.Id,
                    DataCadastro = pedido.DataHora,
                    Peso = pedido.Peso,
                    Latitude = pedido.Latitude,
                    Longitude = pedido.Longitude,
                    Status = pedido.Status
                });
            }

            return pedidosView;
        }
        public async Task<PedidoViewModel> ObterPedido(Guid id)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido != null)
            {
                var pedidoView = new PedidoViewModel
                {
                    Id = pedido.Id,
                    DataCadastro = pedido.DataHora,
                    Peso = pedido.Peso,
                    Latitude = pedido.Latitude,
                    Longitude = pedido.Longitude,
                    Status = pedido.Status
                };

                return pedidoView;
            }

            return null;
        }
    }
}
