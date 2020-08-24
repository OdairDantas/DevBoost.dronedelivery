using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Interfaces.Services;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Application.Handles
{
    public class EntregaHandler : INotificationHandler<SolicitarEntregaEvent>
    {
        private IUnitOfWork _unitOfWork;
        private IPedidoService _pedidoService;

        public EntregaHandler(IUnitOfWork unitOfWork, IPedidoService pedidoService)
        {
            _unitOfWork = unitOfWork;
            _pedidoService = pedidoService;
        }

        public async Task Handle(SolicitarEntregaEvent messagem, CancellationToken cancellationToken)
        {

            //TODO: Executar entraga...
            await CheckOut();
        }
        public async Task CheckOut()
        {
            var pedidos = await Task.FromResult(_pedidoService.ObterTodos().Result.ToList());
            var dronesDisponiveis = await Task.FromResult(_unitOfWork.DroneItinerario.GetAllAsync().Result?.ToList().Where(x => x.StatusDrone == EStatusDrone.Disponivel));

            var entregas = new List<Entrega>();

            foreach (var item in dronesDisponiveis)
            {
                var drone = await Task.FromResult(_unitOfWork.Drone.GetAllAsync().Result.ToList().FirstOrDefault(x => x.Id == item.DroneId));
                var listaCheckout = pedidos.Where(p => p.Peso <= drone.Capacidade);
                listaCheckout = listaCheckout?.Where(p => drone.AutonomiaRestante >= new Localizacao() { Latitude = (double)p.Latitude, Longitude = (double)p.Longitude }.CalcularDistanciaEmKilometros() * 2);

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
