using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Handles;
using DevBoost.DroneDelivery.Domain.Interfaces.Repositories;
using DevBoost.DroneDelivery.Domain.Menssages;
using DevBoost.DroneDelivery.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Contexts
{
    public class DroneDeliveryContext : DbContext, IUnitOfWork
    {
        private readonly IMediatrHandler _bus;
        public DroneDeliveryContext(DbContextOptions<DroneDeliveryContext> options, IMediatrHandler bus) : base(options)
        {
            _bus = bus;
        }

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Drone> Drone { get; set; }
        public DbSet<Entrega> Entrega { get; set; }
        public DbSet<DroneItinerario> DroneItinerario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DroneDeliveryContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DroneDelivery;Trusted_Connection=true;");
            }
        }


        public async Task<bool> Commit()
        {

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _bus.PublicarEventos(this);

            return sucesso;
        }

    }
}
