using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CarRentApp
{
    public interface IDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DatabaseFacade Database { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class CarRentDbContext: DbContext, IDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Sedan> Sedans { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Van> Vans { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder?.UseSqlServer(ConfigurationManager.ConnectionStrings["SQL"].ConnectionString);
            optionsBuilder?.UseSqlServer("Server = (local); Database = CarRent; Trusted_Connection = True; MultipleActiveResultSets = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarRental>().HasIndex(e => e.CarId).IsUnique(); 
            modelBuilder.Entity<Car>().OwnsOne(c => c.AirConditioner);
            modelBuilder.Entity<Car>().OwnsOne(c => c.Radio);
            modelBuilder.Entity<Car>()
                .HasDiscriminator<CarType>("Type")
                .HasValue<Sedan>(CarType.Sedan)
                .HasValue<Sport>(CarType.Sport)
                .HasValue<Van>(CarType.Van);
        }
    }
}
