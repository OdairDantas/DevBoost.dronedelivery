using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System;

namespace DevBoost.DroneDelivery.Application.Validations
{
    public class AdicionarDroneValidation: AbstractValidator<AdicionarDroneCommand>
    {
        public AdicionarDroneValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do Pedido inválido");

            RuleFor(c => c.Velocidade)
                .NotEmpty()
                .WithMessage("A Velocidade não foi informada");

            RuleFor(c => c.Autonomia)
                .NotEmpty()
                .WithMessage("Autonomia não foi informada");

            RuleFor(c => c.Capacidade)
                .NotEmpty()
                .WithMessage("Capacidade não foi informada");

            RuleFor(c => c.Carga)
                .NotEmpty()
                .WithMessage("Carga não informada");

            RuleFor(c => c.Capacidade)
                .LessThanOrEqualTo(12000)
                .WithMessage("Capacidade não foi informada.");
        }
    }
}
