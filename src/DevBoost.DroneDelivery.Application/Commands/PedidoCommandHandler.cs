using DevBoost.DroneDelivery.Application.Queries;
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
    public class PedidoCommandHandler : IRequestHandler<AdicionarPedidoCommand, bool>, IRequestHandler<EntregarPedidoCommand, bool>
    {
        private IDroneQueries _droneQueries;
        private IPedidoQueries _pedidoQueries;
        private IPedidoRepository _pedidoRepository;
        private IDroneItinerarioRepository _itinerarioRepository;
        private IEntregaRepository _entregaRepository;
        private readonly IMediatrHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoQueries pedidoQueries, IDroneQueries droneQueries, IPedidoRepository pedidoRepository, IDroneItinerarioRepository itinerarioRepository, IEntregaRepository entregaRepository, IMediatrHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _itinerarioRepository = itinerarioRepository;
            _entregaRepository = entregaRepository;
            _mediatorHandler = mediatorHandler;
            _droneQueries = droneQueries;
            _pedidoQueries = pedidoQueries;
        }

        public async Task<bool> Handle(AdicionarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var pedido = await _pedidoRepository.GetByIdAsync(message.Id);

            if (pedido == null)
            {
                var novoPedido = new Pedido { Id = message.Id, Latitude = message.Latitude, Longitude = message.Longitude, DataHora = DateTime.Now, Peso = message.Peso, Status = EStatusPedido.AguardandoEntregador };
                await _pedidoRepository.InsertAsync(novoPedido);
            }


            return await _pedidoRepository.UnitOfWork.Commit();

        }


        public async Task<bool> Handle(EntregarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;

            var drone = await _droneQueries.ObterDrone(message.DroneId);
            var pedido = await _pedidoQueries.ObterPedido(message.PedidoId);

            var pedidoAlterado = new Pedido()
            {
                Peso = pedido.Peso,
                Latitude = pedido.Latitude,
                Longitude = pedido.Longitude,
                Id = pedido.Id
            };
            var entrega = new Entrega()
            {
                DataPrevisao = message.DataPrevisao,
                DroneId = message.DroneId,
                PedidoId = message.PedidoId
            };

            var itinerario = new DroneItinerario()
            {
                DroneId = drone.Id,
                StatusDrone = EStatusDrone.EmTransito,
                DataHora = DateTime.Now
            };

            await _itinerarioRepository.InsertAsync(itinerario);
            await _itinerarioRepository.UnitOfWork.Commit();
            await _entregaRepository.InsertAsync(entrega);
            await _entregaRepository.UnitOfWork.Commit();
            pedidoAlterado.Entrega = entrega;
            await _pedidoRepository.UpdateAsync(pedidoAlterado);
            await _pedidoRepository.UnitOfWork.Commit();

            return true;
        }
        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }


    }
}
