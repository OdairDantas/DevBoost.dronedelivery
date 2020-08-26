using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Menssages;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class EntregarPedidoCommand : Command
    {


        public DateTime DataPrevisao { get; private set; }
        public Guid DroneId { get; private set; }
        public Guid PedidoId { get; private set; }
        public EStatusDrone StatusDrone { get; private set; }
        public EStatusPedido StatusPedido { get; private set; }

        public EntregarPedidoCommand(DateTime dataPrevisao, Guid droneId, Guid pedidoId, EStatusDrone statusDrone, EStatusPedido statusPedido)
        {
            DataPrevisao = dataPrevisao;
            DroneId = droneId;
            PedidoId = pedidoId;
            StatusDrone = statusDrone;
            StatusPedido = statusPedido;
        }

        public EntregarPedidoCommand(DateTime dataPrevisao, Guid id, EStatusDrone statusDrone, EStatusPedido statusPedido)
        {
            DataPrevisao = dataPrevisao;
            Id = id;
            StatusDrone = statusDrone;
            StatusPedido = statusPedido;
        }

        public override bool EhValido()
        {
            ValidationResult = new EntregarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
