using System;

namespace CarRentApp
{
    public abstract class Car : Entity
    {
        protected abstract TimeSpan NominalAccelerationTime { get; set; }

        protected virtual Engine Engine { get; set; } = new Engine();
        
        public AirConditioner AirConditioner { get; protected set; }
        public Radio Radio { get; protected set; }
        public string Model { get; set; }
        public abstract CarType Type { get; set; }
        public decimal RentalPrice { get; set; }

        public bool IsEngineOk() => TimeSpan.FromSeconds(Engine.AccelerateTo(100)) <= NominalAccelerationTime;

        protected Car() { }
        protected Car(bool hasRadio, bool hasAirConditioning) : this()
        {
            if (hasRadio) Radio = new Radio();
            if (hasAirConditioning) AirConditioner = new AirConditioner();
        }
    }

    public class Sedan : Car
    {
        protected override TimeSpan NominalAccelerationTime { get; set; } = TimeSpan.FromSeconds(8);
        public override CarType Type { get; set; } = CarType.Sedan;

        public Sedan() { }
        public Sedan(bool hasRadio, bool hasAirConditioning) : base(hasRadio, hasAirConditioning) { }
    }

    public class Sport : Car
    {
        protected override TimeSpan NominalAccelerationTime { get; set; } = TimeSpan.FromSeconds(5);
        protected override Engine Engine { get; set; } = new SportEngine();
        public override CarType Type { get; set; } = CarType.Sport;

        public Sport() { }
        public Sport(bool hasRadio, bool hasAirConditioning) : base(hasRadio, hasAirConditioning) { }
    }

    public class Van : Car
    {
        protected override TimeSpan NominalAccelerationTime { get; set; } = TimeSpan.FromSeconds(13);
        protected override Engine Engine { get; set; } = new PowerfulEngine();
        public override CarType Type { get; set; } = CarType.Van;

        public Van() { }
        public Van(bool hasRadio, bool hasAirConditioning) : base(hasRadio, hasAirConditioning) { }
    }
}