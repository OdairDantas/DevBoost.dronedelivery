using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Handles
{
    public class DroneHandler : INotificationHandler<DroneAutonomiaBaixaEvent>
    {
        private IDroneRepository _droneRepository;
        private IMediatrHandler _bus;
        public DroneHandler(IDroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }
        public async Task Handle(DroneAutonomiaBaixaEvent messagem, CancellationToken cancellationToken)
        {
            var drone = await _droneRepository.GetByIdAsync(messagem.Id);

            if (drone == null) return;
            var command = new CarregarBareriaDroneCommand(capacidade : drone.Capacidade,velocidade:drone.Velocidade,autonomia:drone.Autonomia,autonomiaRestante:drone.AutonomiaRestante,carga:drone.Carga) { };
            await _bus.EnviarComando(command);

        }
    }
}
