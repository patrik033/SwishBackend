using Microsoft.EntityFrameworkCore;
using SwishBackend.Carriers.Models.NearestServicePointDHLModels;
using SwishBackend.Carriers.Models.NearestServicePointResponse;

namespace SwishBackend.Carriers.Data
{
    public class CarriersDbContext : DbContext
    {

        //TODO: fixa relationen för DHL

        //public DbSet<DHLNearestServicePoint>    DHLNearestServicePoints { get; set; }
        public DbSet<PostNordNearestServicePoint> PostNordNearestServicePoints { get; set; }

        public CarriersDbContext(DbContextOptions<CarriersDbContext> options) :base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
