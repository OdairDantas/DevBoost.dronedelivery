using DevBoost.DroneDelivery.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Application.Queries.ViewModel
{
    public class PedidoViewModel
    {
        public Guid Id { get; set; }
        public int Peso { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DataCadastro { get; set; }
        public EStatusPedido Status { get; set; }
    }
}
