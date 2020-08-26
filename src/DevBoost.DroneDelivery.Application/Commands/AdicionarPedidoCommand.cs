using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Domain.Enumerators;
using DevBoost.DroneDelivery.Domain.Menssages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarPedidoCommand : Command
    {
        public int Peso { get; private set; }
        public decimal Latitude { get; private set; }
        public decimal Longitude { get; private set; }
        public EStatusPedido Status { get; private set; }

        public AdicionarPedidoCommand(int peso, decimal latitude, decimal longitude)
        {
            Id = Guid.NewGuid();
            Peso = peso;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
