using System.ComponentModel.DataAnnotations;

namespace CarRentApp
{
    /// <summary>
    /// Stores information about rented cars. It can be extended in future to store rental dates and/or tenant info.
    /// </summary>
    public class CarRental: Entity
    {
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}