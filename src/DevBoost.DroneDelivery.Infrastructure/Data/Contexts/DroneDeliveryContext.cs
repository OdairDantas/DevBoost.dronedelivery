using DevBoost.DroneDelivery.Domain.Entities;
using DevBoost.DroneDelivery.Domain.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DevBoost.DroneDelivery.Infrastructure.Data.Contexts
{
    public class DroneDeliveryContext : DbContext, IDbContext
    {
        public DroneDeliveryContext(DbContextOptions<DroneDeliveryContext> options) : base(options)
        {

        }

        public DroneDeliveryContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


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

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Entrega> Entrega { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Drone> Drone { get; set; }
        public DbSet<DroneItinerario> DroneItinerario { get; set; }

        

    }
}
