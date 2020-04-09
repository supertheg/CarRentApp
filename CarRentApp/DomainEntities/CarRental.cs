using System.ComponentModel.DataAnnotations;

namespace CarRentApp
{
    public class CarRental: Entity
    {
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}