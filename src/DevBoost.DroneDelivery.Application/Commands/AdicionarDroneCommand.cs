using DevBoost.DroneDelivery.Application.Validations;
using DevBoost.DroneDelivery.Domain.Menssages;
using System;

namespace DevBoost.DroneDelivery.Application.Commands
{
    public class AdicionarDroneCommand : Command
    {
        public int Capacidade { get; private set; }
        public int Velocidade { get; private set; }
        public int Autonomia { get; private set; }
        public int AutonomiaRestante { get; private set; }
        public int Carga { get; private set; }

        public AdicionarDroneCommand(int capacidade, int velocidade, int autonomia, int autonomiaRestante, int carga)
        {
            Id = Guid.NewGuid();
            Capacidade = capacidade;
            Velocidade = velocidade;
            Autonomia = autonomia;
            AutonomiaRestante = autonomiaRestante;
            Carga = carga;
        }
        public override bool EhValido()
        {
            ValidationResult = new AdicionarDroneValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
