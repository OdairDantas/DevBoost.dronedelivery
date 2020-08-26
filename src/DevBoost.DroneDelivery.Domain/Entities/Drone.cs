using System.Collections.Generic;

namespace DevBoost.DroneDelivery.Domain.Entities
{
    public class Drone : Entity
    {

        public int Capacidade { get; set; }
        public int Velocidade { get; set; }
        public int Autonomia { get; set; }
        public int AutonomiaRestante { get; set; }
        public int Carga { get; set; }

        public ICollection<Entrega> Entregas { get; set; }
        public ICollection<DroneItinerario> Itinerarios { get; set; }

        
    }
}