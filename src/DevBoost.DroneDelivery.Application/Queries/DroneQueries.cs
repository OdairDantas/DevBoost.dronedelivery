using DevBoost.DroneDelivery.Application.Queries.ViewModel;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Queries
{
    public class DroneQueries : IDroneQueries
    {
        private readonly IDroneItinerarioRepository _itinerarioRepository;
        private readonly IDroneRepository _droneRepository;
        private readonly IEntregaRepository _entregaRepository;
        public DroneQueries(IDroneItinerarioRepository itinerarioRepository, IDroneRepository droneRepository, IEntregaRepository entregaRepository)
        {
            _droneRepository = droneRepository;
            _entregaRepository = entregaRepository;
            _itinerarioRepository = itinerarioRepository;
        }

        public async Task<DroneViewModel> ObterDrone(Guid id)
        {
            var drone = await _droneRepository.GetByIdAsync(id);
            if (drone == null) return null;

            return new DroneViewModel()
            {
                Id = drone.Id,
                Autonomia = drone.Autonomia,
                AutonomiaRestante = drone.AutonomiaRestante,
                Capacidade = drone.Capacidade,
                Carga = drone.Carga,
                Velocidade = drone.Velocidade
            };


        }

        public async Task<IEnumerable<DroneViewModel>> ObterDrones()
        {
            var drones = await _droneRepository.GetAllAsync();
            if (!drones.Any()) return null;

            var droneViewModels = new List<DroneViewModel>();
            foreach (var drone in drones)
            {
                droneViewModels.Add(new DroneViewModel()
                {
                    Id = drone.Id,
                    Autonomia = drone.Autonomia,
                    AutonomiaRestante = drone.AutonomiaRestante,
                    Capacidade = drone.Capacidade,
                    Carga = drone.Carga,
                    Velocidade = drone.Velocidade
                });
            }

            return droneViewModels;
        }
        public async Task<DroneViewModel> ObterDroneDisponiveil()
        {
            var itinerarios = await _itinerarioRepository.GetAllAsync();
            var statusDisponivel = itinerarios.ToList().OrderBy(d => d.DataHora).Last(i => i.StatusDrone == EStatusDrone.Disponivel);
            if (statusDisponivel == null) return null;
            var droneDisponiveil = await ObterDrone(statusDisponivel.DroneId);

            return droneDisponiveil;

        }
        public async Task<SituacaoViewModel> ObterSituacao(Guid id)
        {
            var drone = await _droneRepository.ObterSituacao(id);

            if (drone.Entregas.Any())
            {
                var entransito = drone.Entregas.Where(e => e.DataPrevisao > DateTime.Now);
                var itinerario = drone.Itinerarios.Last();
                var situacaoViewModel = new SituacaoViewModel() { Drone = drone.Id, Pedidos = entransito.Select(e => e.Pedido), Itinerarios = itinerario };

                return situacaoViewModel;
            }

            return null;
        }
    }
}
