using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Services
{
    public class DroneService : IDroneService
    {

        private readonly IUnitOfWork _unitOfWork;


        public DroneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Drone> Criar(Drone drone)
        {
            await Task.FromResult(_unitOfWork.Drone.InsertAsync(drone));
            return drone;
        }
        public async Task<IEnumerable<Drone>> ObterTodos()
        {
            return await Task.FromResult(_unitOfWork.Drone.GetAllAsync().Result);
        }
        public async Task<Drone> ObterByIdAsync(Guid id)
        {
            return await _unitOfWork.Drone.GetByIdAsync(id);
        }
        public async Task<Drone> UpdateAsync(Drone drone)
        {
            if (!DroneExiste(drone.Id))
                return null;

            await _unitOfWork.Drone.UpdateAsync(drone);
            await _unitOfWork.SaveAsync();

            return drone;

        }

        public async Task<Drone> DeleteAsync(Guid id)
        {
            var drone = await _unitOfWork.Drone.GetByIdAsync(id);
            if (drone == null)
                return null;


            await _unitOfWork.Drone.DeleteAsync(drone);
            await _unitOfWork.SaveAsync();

            return drone;

        }
        public bool DroneExiste(Guid id)
        {
            var drone = _unitOfWork.Drone.GetByIdAsync(id);

            if (drone == null)
                return false;

            return true;
        }


        public async Task AtualizarStatusDronesAsync()
        {
            var droneItinerarios = _unitOfWork.DroneItinerario.GetAllAsync().Result.Where(d => d.StatusDrone != EStatusDrone.Disponivel).ToList();

            foreach (var droneItinerario in droneItinerarios)
            {
                droneItinerario.Drone = await Task.FromResult(_unitOfWork.Drone.GetByIdAsync(droneItinerario.DroneId).Result);

                if (droneItinerario.StatusDrone == EStatusDrone.Carregando)
                {
                    if (DateTime.Now.Subtract(droneItinerario.DataHora).Minutes >= 60)
                    {
                        droneItinerario.StatusDrone = EStatusDrone.Disponivel;
                        droneItinerario.Drone.AutonomiaRestante = droneItinerario.Drone.Autonomia;
                        droneItinerario.DataHora = DateTime.Now;
                        await _unitOfWork.DroneItinerario.InsertAsync(droneItinerario);
                        await _unitOfWork.SaveAsync();
                    }
                }


            }

        }


    }
}
