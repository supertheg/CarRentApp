namespace CarRentApp
{
    public class Car: Entity
    {
        public CarType Type { get; set; }
        public string Model { get; set; }
        public bool AirConditioning { get; set; }
        public bool Radio { get; set; }
        public decimal RentalPrice { get; set; }
    }
}