using System;

namespace CarRentApp
{
    /// <summary>
    /// Describes car type with type-specific attributes
    /// </summary>
    public class CarType: Entity
    {
        public string Name { get; set; }
        public TimeSpan AccelerationTime { get; set; }
    }
}