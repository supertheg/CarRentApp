using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarRentApp
{
    public interface ICarService
    {
        IEnumerable<CarViewModel> SearchCars(CriteriaViewModel filter);
        IEnumerable<string> GetCarTypes();
        RentResults RentCar(int id);
    }

    public class CarService : ICarService
    {
        private readonly IDbContext context;

        public IEnumerable<CarViewModel> SearchCars(CriteriaViewModel filter)
        {
            var result = context.Cars
                .Include(c => c.Type)
                .Where(c => string.IsNullOrEmpty(filter.TypeName) || c.Type.Name == filter.TypeName)
                .Where(c => filter.Conditioning == false || c.AirConditioning)
                .Where(c => filter.Radio == false || c.Radio)
                .Where(c => !filter.PriceFrom.HasValue || filter.PriceFrom.Value <= c.RentalPrice)
                .Where(c => !filter.PriceTo.HasValue || c.RentalPrice <= filter.PriceTo.Value)
                .Select(c => new CarViewModel {
                    Id = c.Id,
                    Model = c.Model,
                    TypeName = c.Type.Name,
                    AccelerationTime = c.Type.AccelerationTime.TotalSeconds,
                    AirConditioning = c.AirConditioning,
                    Radio = c.Radio,
                    RentalPrice = c.RentalPrice
                })
                .ToList();

            return result;
        }

        public IEnumerable<string> GetCarTypes() => context.CarTypes.Select(t => t.Name).ToList();

        public RentResults RentCar(int id)
        {
            if (context.CarRentals.Any(r => r.Car.Id == id)) {
                return RentResults.AlreadyRented;
            }

            context.CarRentals.Add(new CarRental { CarId = id });
            context.SaveChanges();

            return RentResults.RentSuccessful;
        }

        public CarService(IDbContext context) => this.context = context;

    }
}