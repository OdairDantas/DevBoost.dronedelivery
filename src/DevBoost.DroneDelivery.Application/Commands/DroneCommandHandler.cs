using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Menssages;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class DroneCommandHandler : IRequestHandler<AdicionarDroneCommand, bool>, IRequestHandler<CarregarBareriaDroneCommand, bool>
    {
        private IDroneItinerarioRepository _itinerarioRepository;
        private IDroneRepository _droneRepository;
        private IMediatrHandler _bus;

        public DroneCommandHandler(IDroneItinerarioRepository itinerarioRepository, IDroneRepository droneRepository, IMediatrHandler bus)
        {
            _itinerarioRepository = itinerarioRepository;
            _droneRepository = droneRepository;
            _bus = bus;
        }

        public async Task<bool> Handle(AdicionarDroneCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var drone = await _droneRepository.GetByIdAsync(message.Id);
            if (drone == null)
            {
                var novodrone = new Drone
                {
                    Id = message.Id,
                    Autonomia = message.Autonomia,
                    AutonomiaRestante = message.AutonomiaRestante,
                    Capacidade = message.Capacidade,
                    Carga = message.Carga,
                    Velocidade = message.Velocidade
                };
                await _droneRepository.InsertAsync(novodrone);
            }

            return await _droneRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CarregarBareriaDroneCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var drone = await _droneRepository.GetByIdAsync(message.Id);

            if (drone == null)
            {
                var itinerario = new DroneItinerario() { DataHora = DateTime.Now, Drone = drone, DroneId = drone.Id, StatusDrone = EStatusDrone.Carregando };
                await _itinerarioRepository.InsertAsync(itinerario);
            }

            return await _droneRepository.UnitOfWork.Commit();
        }
        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
