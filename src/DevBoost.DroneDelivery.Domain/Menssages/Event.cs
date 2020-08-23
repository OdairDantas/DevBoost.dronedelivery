using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Domain.Menssages
{
    public abstract class Event : Menssage, INotification
    {
        protected Event(Guid entityId)
        {
            Timestamp = DateTime.Now;
            EntityId = entityId;
        }

        public DateTime Timestamp { get; private set; }
        public Guid EntityId { get; set; }
    }
}
