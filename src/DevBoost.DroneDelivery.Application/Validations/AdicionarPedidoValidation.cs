using DevBoost.DroneDelivery.Application.Commands;
using DevBoost.DroneDelivery.Application.Extensions;
using DevBoost.DroneDelivery.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Validations
{
    public class AdicionarPedidoValidation : AbstractValidator<AdicionarPedidoCommand>
    {
        public AdicionarPedidoValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do Pedido inválido");

            RuleFor(c => c.Latitude)
                .NotEmpty()
                .WithMessage("O Latitude não foi informado");

            RuleFor(c => c.Longitude)
                .NotEmpty()
                .WithMessage("O Longitude não foi informado");

            RuleFor(c => c.Peso)
                .LessThanOrEqualTo(12000)
                .WithMessage("Rejeitado: Pedido acima do peso máximo aceito.");

            RuleFor(c => ((new Localizacao() { Latitude = (double)c.Latitude, Longitude = (double)c.Longitude }.CalcularDistanciaEmKilometros() * 2) / 35) * 60)
                .LessThanOrEqualTo(35)
                .WithMessage("Rejeitado: Fora da área de entrega.");

        }

    }
}
