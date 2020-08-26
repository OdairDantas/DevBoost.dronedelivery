using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class DroneItinerarioMapping : IEntityTypeConfiguration<DroneItinerario>
    {
        public void Configure(EntityTypeBuilder<DroneItinerario> builder)
        {
            builder.HasKey(d => d.Id);

            
            builder.HasOne(i => i.Drone)
                .WithMany(d => d.Itinerarios);

            builder.ToTable("DroneItinerario");
        }
    }
}
