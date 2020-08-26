using DevBoost.DroneDelivery.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Application.Validations
{
    public class EntregarPedidoValidation : AbstractValidator<EntregarPedidoCommand>
    {
        public EntregarPedidoValidation()
        {
            //TODO: adicionar validações para entrega...
        }
    }
}
