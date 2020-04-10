namespace CarRentApp
{
    public class AirConditioner
    {
        private int TemperatureIncrement { get; set; } = 1;

        public int CurrentTemperature { get; private set; } = 25;

        public void TurnUp() => CurrentTemperature += TemperatureIncrement;
        public void TurnDown() => CurrentTemperature -= TemperatureIncrement;
    }
}