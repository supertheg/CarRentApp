using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CarRentApp
{
    [Flags]
    [TypeConverter(typeof(EnumDisplayNameConverter))]
    public enum RentResults
    {
        None,
        [Display(Name = "Rent Successful")] RentSuccessful,
        [Display(Name = "Already Rented")] AlreadyRented,
        [Display(Name = "Engine Fail")] EngineFail,

        [Display(Name = "Air Conditioning Fail")]
        AirConditioningFail,
        [Display(Name = "Radio Fail")] RadioFail
    }

    public interface IRentCarService
    {
        Task<RentResults> RentCarAsync(int id);
    }

    public class RentCarService : IRentCarService
    {
        static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly IDbContext context;
        private readonly Technician technician;

        public async Task<RentResults> RentCarAsync(int id)
        {
            await SemaphoreSlim.WaitAsync();
            try {

                if (await context.CarRentals.AnyAsync(r => r.CarId == id))
                    return RentResults.AlreadyRented;

                var car = await context.Cars.FindAsync(id);
                var checkResult = technician.CheckMachineHealth(car);
                if (checkResult != RentResults.None)
                    return checkResult;
                
                context.CarRentals.Add(new CarRental { CarId = id });
                await context.SaveChangesAsync();
                return RentResults.RentSuccessful;
            }
            finally {
                SemaphoreSlim.Release();
            }
        }

        public RentCarService(IDbContext context, Technician technician)
        {
            this.context = context;
            this.technician = technician;
        }
    }
}