using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {


        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(c => c.Id);

            //// 1 : N => Pedido : Pagamento
            //builder.HasOne(c => c.Pedido)
            //    .WithMany(c => c.PedidoItems);

            builder.ToTable("Pedido");

        }
    }
}
