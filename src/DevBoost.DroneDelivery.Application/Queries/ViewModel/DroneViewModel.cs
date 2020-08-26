using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Queries.ViewModel
{
    public class DroneViewModel
    {
        public Guid Id { get; set; }
        public int Capacidade { get; set; }
        public int Velocidade { get; set; }
        public int Autonomia { get; set; }
        public int AutonomiaRestante { get; set; }
        public int Carga { get; set; }
    }
}
