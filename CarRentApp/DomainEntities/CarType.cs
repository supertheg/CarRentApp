using System;

namespace CarRentApp
{
    public class CarType: Entity
    {
        public string Name { get; set; }
        public TimeSpan AccelerationTime { get; set; }
    }
}