using System.Collections.Generic;

namespace CarRentApp
{
    public abstract class PreflightChecker
    {
        protected readonly IEnumerable<ICarSpecification<Car>> CheckList;

        protected PreflightChecker(IEnumerable<ICarSpecification<Car>> checkList) => CheckList = checkList;

        public RentResults CheckMachineHealth(Car car)
        {
            foreach (var specification in CheckList) {
                if (!specification.IsSatisfied(car)) return specification.FailRentResults;
            }

            return RentResults.None;
        }
    }

    public class Technician : PreflightChecker
    {
        public Technician(IEnumerable<ICarSpecification<Car>> checkList) : base(checkList)
        { }
    }
}