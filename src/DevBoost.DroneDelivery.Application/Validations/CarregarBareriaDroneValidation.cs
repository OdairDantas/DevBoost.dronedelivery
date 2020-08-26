using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Validations
{
    public class CarregarBareriaDroneValidation: AbstractValidator<CarregarBareriaDroneCommand>
    {
        public CarregarBareriaDroneValidation()
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

            RuleFor(c => c.AutonomiaRestante)
                .GreaterThan(20)
                .WithMessage("Carga da bateria não necessaria.");
        }
    }
}
