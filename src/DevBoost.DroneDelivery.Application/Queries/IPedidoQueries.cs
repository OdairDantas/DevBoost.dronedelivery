using DevBoost.DroneDelivery.Application.Queries.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<IEnumerable<PedidoViewModel>> ObterPedidosAguardandoEntregador();
        Task<PedidoViewModel> ObterPedido(Guid id);
        Task<IEnumerable<PedidoViewModel>> ObterPedidos();
    }
}
