using DevBoost.DroneDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Mappings
{
    public class DroneMapping : IEntityTypeConfiguration<Drone>
    {
        public void Configure(EntityTypeBuilder<Drone> builder)
        {
            builder.HasKey(c => c.Id);

            builder.ToTable("Drone");
        }
    }
}
