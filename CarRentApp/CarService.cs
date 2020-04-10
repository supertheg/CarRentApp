using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp
{
    public interface ISearchCarService
    {
        Task<IEnumerable<CarViewModel>> SearchCarsAsync(CriteriaViewModel filter);
        IEnumerable<CarType> GetCarTypes();
    }

    public class SearchCarService : ISearchCarService
    {
        private readonly IDbContext context;

        public async Task<IEnumerable<CarViewModel>> SearchCarsAsync(CriteriaViewModel filter) =>
            await context.Cars
                .Where(c => filter.Type == CarType.None || c.Type == filter.Type)
                .Where(c => filter.Conditioning == false || c.AirConditioner != null)
                .Where(c => filter.Radio == false || c.Radio != null)
                .Where(c => !filter.PriceFrom.HasValue || filter.PriceFrom.Value <= c.RentalPrice)
                .Where(c => !filter.PriceTo.HasValue || c.RentalPrice <= filter.PriceTo.Value)
                .Select(c => new CarViewModel {
                    Id = c.Id,
                    Model = c.Model,
                    Type = c.Type,
                    AirConditioning = c.AirConditioner != null,
                    Radio = c.Radio != null,
                    RentalPrice = c.RentalPrice
                })
                .ToListAsync();

        public IEnumerable<CarType> GetCarTypes() => Enum.GetValues(typeof(CarType)).Cast<CarType>();

        public SearchCarService(IDbContext context) => this.context = context;
    }
}