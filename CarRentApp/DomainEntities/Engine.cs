namespace CarRentApp
{
    public class Engine
    {
        /// <summary>
        /// Acceleration in m/s2
        /// </summary>
        public double Acceleration { get; set; } = 3.5;

        /// <summary>
        /// Speeds up the engine up to the desired velocity and returns elapsed time in seconds
        /// </summary>
        /// <param name="speed">Speed in km/h</param>
        /// <returns></returns> 
        public double AccelerateTo(double speed)
        {
            return ConvertSpeedToMpS(speed) / Acceleration;
        }

        private double ConvertSpeedToMpS(double speed) => speed / 3.6;
    }

    public class SportEngine : Engine
    {
        public SportEngine()
        {
            Acceleration = 5.6;
        }
    }

    public class PowerfulEngine : Engine
    {
        public PowerfulEngine()
        {
            Acceleration = 2.2;
        }
    }
}