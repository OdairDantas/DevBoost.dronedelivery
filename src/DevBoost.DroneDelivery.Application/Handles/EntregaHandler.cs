using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Events;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Application.Queries;
using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
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
        private IDroneRepository _droneRepository;
        private IPedidoQueries _pedidoQueries;
        private IDroneQueries _droneQueries;
        private IMediatrHandler _bus;

        public EntregaHandler(IDroneRepository droneRepository, IPedidoQueries pedidoQueries, IDroneQueries droneQueries, IMediatrHandler bus)
        {
            _droneRepository = droneRepository;
            _pedidoQueries = pedidoQueries;
            _droneQueries = droneQueries;
            _bus = bus;
        }

        public async Task Handle(SolicitarEntregaEvent messagem, CancellationToken cancellationToken)
        {

            await CheckOut();
        }
        public async Task CheckOut()
        {
            var pedidos = await _pedidoQueries.ObterPedidosAguardandoEntregador();

            var entregas = new List<Entrega>();

            foreach (var item in pedidos)
            {
                var drone = await _droneQueries.ObterDroneDisponiveil();

                var listaCheckout = pedidos.Where(p => p.Peso <= drone.Capacidade);
                listaCheckout = listaCheckout?.Where(p => drone.AutonomiaRestante >= new Localizacao() { Latitude = (double)p.Latitude, Longitude = (double)p.Longitude }.CalcularDistanciaEmKilometros() * 2);

                foreach (var checkout in listaCheckout)
                {
                    var tempoPercurso = Convert.ToInt32(new Localizacao() { Latitude = (double)checkout.Latitude, Longitude = (double)checkout.Longitude }.CalcularDistanciaEmKilometros() * 2);

                    if ((drone.Capacidade >= checkout.Peso) && (drone.AutonomiaRestante >= tempoPercurso))
                    {
                        entregas.Add(new Entrega() { PedidoId = checkout.Id, DroneId = drone.Id, DataPrevisao = DateTime.Now.AddMinutes(tempoPercurso / 2) });

                        var command = new EntregarPedidoCommand(dataPrevisao: DateTime.Now.AddMinutes(tempoPercurso / 2), checkout.Id, statusDrone: EStatusDrone.EmTransito, statusPedido: EStatusPedido.EmTransito);
                        var saiuParaEntrega = await _bus.EnviarComando(command);
                        if (saiuParaEntrega)
                        {
                            drone.Capacidade -= checkout.Peso;
                            drone.AutonomiaRestante -= tempoPercurso;

                        }
                    }
                    else
                    {
                        break;

                    }
                }

                var droneAlterado = await _droneRepository.GetByIdAsync(drone.Id);
                droneAlterado.AutonomiaRestante = drone.AutonomiaRestante;
                await _droneRepository.UpdateAsync(droneAlterado);

            }
            
        }
    }

}
