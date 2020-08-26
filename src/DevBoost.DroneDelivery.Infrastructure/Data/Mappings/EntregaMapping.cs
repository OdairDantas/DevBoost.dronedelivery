using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class EntregaMapping : IEntityTypeConfiguration<Entrega>
    {
        public void Configure(EntityTypeBuilder<Entrega> builder)
        {
            builder.HasKey(e => new { e.Id, e.PedidoId });

            builder.HasOne(e => e.Pedido)
                .WithOne(p => p.Entrega);

            builder.HasOne(e => e.Drone)
                .WithMany(d => d.Entregas);

            builder.ToTable("Entrega");
        }
    }
}
