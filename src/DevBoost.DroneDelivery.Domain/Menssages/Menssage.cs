using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Domain.Menssages
{
    public abstract class Menssage
    {
        public string MessageType { get; protected set; }
        public Guid Id { get; set; }

        protected Menssage()
        {
            MessageType = GetType().Name;
        }

    }
}
