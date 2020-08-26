using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    public class Entrega : Entity
    {
        public DateTime DataPrevisao { get; set; }
        public Guid   DroneId { get; set; }
        public Guid  PedidoId { get; set; }

        public Drone Drone { get; set; }
        public Pedido Pedido { get; set; }

    }
}
