using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentApp
{
    public interface IDataGenerationService
    {
        Task SeedDatabaseAsync();
    }

    public class DataGenerationService : IDataGenerationService
    {
        private readonly string[] carModels = {
            "Acura", "Alfa Romeo", "Aston Martin", "Audi", "Bentley", "BMW", "Bugatti", "Buick", "Cadillac", "Chevrolet", "Chrysler", "Citroen", "Cupra", "Dodge", "Ferrari", "Fiat", "Ford", "Genesis", "GMC", "Honda", "Hyundai", "Infiniti", "Jaguar", "Jeep", "Kia", "Koenigsegg", "Lamborghini",
            "Land Rover", "Lexus", "Lincoln", "Lotus", "Maserati", "Mazda", "McLaren", "Mercedes-Benz", "Mini", "Mitsubishi", "Nissan", "Opel", "Pagani", "Peugeot", "Porsche", "Ram", "Renault", "Rolls-Royce", "Seat", "Subaru", "Suzuki", "Tata Motors", "Tesla", "Toyota", "Vauxhall", "Volkswagen",
            "Volvo"
        };

        private readonly IDbContext context;

        public async Task SeedDatabaseAsync()
        {
            context.CarRentals.RemoveRange(context.CarRentals);
            context.Cars.RemoveRange(context.Cars);

            var random = new Random();

            var cars = Enumerable.Range(0, 1000).Select(n => {
                    Car car;
                    (bool radio, bool cond) = (random.Next(2) == 1, random.Next(2) == 1);

                    switch (random.Next(3)) {
                        case 1:
                            car = new Sport(radio, cond);
                            break;
                        case 2:
                            car = new Van(radio, cond);
                            break;
                        default:
                            car = new Sedan(radio, cond);
                            break;
                    }

                    car.Model = carModels[random.Next(carModels.Length)];
                    car.RentalPrice = random.Next(10, 150);
                    return car;
                }
            ).ToList();

            context.Cars.AddRange(cars);
            context.CarRentals.AddRange(
                Enumerable.Range(0, random.Next(0, 500)).Select(n => new CarRental { Car = cars[n] })
            );

            await context.SaveChangesAsync();
        }

        public DataGenerationService(IDbContext context) => this.context = context;
    }
}