using DevBoost.DroneDelivery.Domain.Menssages;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Events
{
    public class DroneAutonomiaBaixaEvent : Event
    {
        public DroneAutonomiaBaixaEvent(Guid entityId) : base(entityId)
        {
        }
    }
}
