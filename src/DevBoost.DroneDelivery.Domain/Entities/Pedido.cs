using DevBoost.DroneDelivery.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    public class Pedido : Entity
    {

        public int Peso { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime DataHora { get; set; }
        public EStatusPedido Status { get; set; }
    }
}
