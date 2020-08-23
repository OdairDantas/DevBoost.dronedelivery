using DevBoost.DroneDelivery.Domain.Enumerators;
using System;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    public class DroneItinerario:Entity
    {
        public DateTime DataHora { get; set; }
        public Drone Drone { get; set; }
        public Guid DroneId { get; set; }
        public EStatusDrone StatusDrone { get; set; }

    }
}
