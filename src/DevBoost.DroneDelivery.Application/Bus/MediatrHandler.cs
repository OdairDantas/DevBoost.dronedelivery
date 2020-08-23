using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Menssages;
using MediatR;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}
