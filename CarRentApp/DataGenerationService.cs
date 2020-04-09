using System;
using System.Linq;

namespace CarRentApp
{
    public interface IDataGenerationService
    {
        void SeedDatabase();
    }

    public class DataGenerationService : IDataGenerationService
    {
        private readonly CarType[] carTypes = {
            new CarType { Name = "sport", AccelerationTime = TimeSpan.FromSeconds(5) },
            new CarType { Name = "sedan", AccelerationTime = TimeSpan.FromSeconds(8) },
            new CarType { Name = "van", AccelerationTime = TimeSpan.FromSeconds(13) }
        };

        private readonly string[] carModels = {
            "Acura", "Alfa Romeo", "Aston Martin", "Audi", "Bentley", "BMW", "Bugatti", "Buick", "Cadillac", "Chevrolet", "Chrysler", "Citroen", "Cupra", "Dodge", "Ferrari", "Fiat", "Ford", "Genesis", "GMC", "Honda", "Hyundai", "Infiniti", "Jaguar", "Jeep", "Kia", "Koenigsegg", "Lamborghini",
            "Land Rover", "Lexus", "Lincoln", "Lotus", "Maserati", "Mazda", "McLaren", "Mercedes-Benz", "Mini", "Mitsubishi", "Nissan", "Opel", "Pagani", "Peugeot", "Porsche", "Ram", "Renault", "Rolls-Royce", "Seat", "Subaru", "Suzuki", "Tata Motors", "Tesla", "Toyota", "Vauxhall", "Volkswagen",
            "Volvo"
        };

        private readonly IDbContext context;

        public void SeedDatabase()
        {
            context.CarRentals.RemoveRange(context.CarRentals);
            context.Cars.RemoveRange(context.Cars);
            context.CarTypes.RemoveRange(context.CarTypes);
            context.SaveChanges();

            context.CarTypes.AddRange(carTypes);

            var random = new Random();

            var cars = Enumerable.Range(0, 1000).Select(n =>
                new Car {
                    Type = carTypes[random.Next(3)],
                    Model = carModels[random.Next(carModels.Length)],
                    AirConditioning = random.Next(2) == 1,
                    Radio = random.Next(2) == 1,
                    RentalPrice = random.Next(10, 150)
                }
            ).ToList();

            context.Cars.AddRange(cars);

            context.CarRentals.AddRange(
                Enumerable.Range(0, random.Next(0, 500)).Select(n => new CarRental { Car = cars[n] })
            );

            context.SaveChanges();
        }

        public DataGenerationService(IDbContext context) => this.context = context;
    }
}