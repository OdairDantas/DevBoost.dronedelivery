using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Services
{
    public class PedidoService : IPedidoService
    {

        private readonly IUnitOfWork _unitOfWork;
        private IMediatrHandler _bus;
        private IDroneService _droneService;
        public PedidoService(IDroneService droneService, IUnitOfWork unitOfWork, IMediatrHandler bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
            _droneService = droneService;
        }

        public async Task<Pedido> Criar(Pedido pedido)
        {
            await _unitOfWork.Pedidos.InsertAsync(pedido);
            await _unitOfWork.SaveAsync();
            await CheckOut();

            return pedido;
        }
        public async Task<IEnumerable<Pedido>> ObterTodos()
        {
            return await Task.FromResult(_unitOfWork.Pedidos.GetAllAsync().Result);
        }
        public async Task<Pedido> ObterByIdAsync(Guid id)
        {
            return await _unitOfWork.Pedidos.GetByIdAsync(id);
        }
        public async Task<Pedido> UpdateAsync(Pedido pedido)
        {
            if (!PedidoExiste(pedido.Id))
                return null;

            await _unitOfWork.Pedidos.UpdateAsync(pedido);
            await _unitOfWork.SaveAsync();

            return pedido;

        }

        public async Task<Pedido> DeleteAsync(Guid id)
        {
            var pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
            if (pedido == null)
            {
                return null;
            }

            await _unitOfWork.Pedidos.DeleteAsync(pedido);
            await _unitOfWork.SaveAsync();

            return pedido;

        }
        public bool PedidoExiste(Guid id)
        {
            var pedido = _unitOfWork.Pedidos.GetByIdAsync(id);

            if (pedido == null)
                return false;

            return true;
        }


        public async Task CheckOut()
        {
            var pedidos = await Task.FromResult(ObterTodos().Result.ToList());
            var dronesDisponiveis = await Task.FromResult(_unitOfWork.DroneItinerario.GetAllAsync().Result?.ToList().Where(x => x.StatusDrone == EStatusDrone.Disponivel));
            
            var entregas = new List<Entrega>();

            foreach (var item in dronesDisponiveis)
            {
                var drone = await Task.FromResult(_unitOfWork.Drone.GetAllAsync().Result.ToList().FirstOrDefault(x=>x.Id== item.DroneId));
                var listaCheckout = pedidos.Where(p => p.Peso <= drone.Capacidade);
                listaCheckout = listaCheckout?.Where(p=> drone.AutonomiaRestante >= new Localizacao() { Latitude = (double)p.Latitude, Longitude = (double)p.Longitude }.CalcularDistanciaEmKilometros() * 2);
                
                foreach (var checkout in listaCheckout)
                {
                    var tempoPercurso = Convert.ToInt32(new Localizacao() { Latitude = (double)checkout.Latitude, Longitude = (double)checkout.Longitude }.CalcularDistanciaEmKilometros() * 2);

                    if ((drone.Capacidade >= checkout.Peso) && (drone.AutonomiaRestante >= tempoPercurso))
                    {
                        entregas.Add(new Entrega() { PedidoId = checkout.Id, DroneId = drone.Id, DataPrevisao = DateTime.Now.AddMinutes(tempoPercurso / 2) });
                        drone.Capacidade -= checkout.Peso;
                        drone.AutonomiaRestante -= tempoPercurso;
                    }
                    else
                    {
                        break;

                    }
                }

            }
            if (entregas.Any())
            {
                await _unitOfWork.Entrega.InsertCollectionAsync(entregas);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
