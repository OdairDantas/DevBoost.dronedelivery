using DevBoost.DroneDelivery.Domain.Menssages;
using System;

namespace DevBoost.DroneDelivery.Application.Events
{
    public class SolicitarEntregaEvent : Event
    {

        public SolicitarEntregaEvent(Guid guid): base(guid)
        {

        }
    }
}
