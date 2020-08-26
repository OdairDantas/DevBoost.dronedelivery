using DevBoost.DroneDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Queries.ViewModel
{
    public class SituacaoViewModel
    {

        public Guid Drone { get; set; }
        public IEnumerable<Pedido>  Pedidos { get; set; }
        public DroneItinerario Itinerarios { get; set; }
    }
}
