namespace CarRentApp
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Model { get; set; }
        public bool AirConditioning { get; set; }
        public bool Radio { get; set; }
        public decimal RentalPrice { get; set; }
        public double AccelerationTime { get; set; }
    }
}