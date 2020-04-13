namespace CarRentApp
{
    public interface ICarSpecification<in T>
    {
        bool IsSatisfied(T item);
        RentResults FailRentResults { get; }
    }

    public class RadioSpecification : ICarSpecification<Car>
    {
        public bool IsSatisfied(Car car)
        {
            var radio = car.Radio;
            if (radio != null) {
                radio.On();
                if (!radio.IsRadioOn)
                    return false;
                radio.SwitchChannel(Radio.Channel.EuropaPlus);
                if (radio.CurrentChannel != Radio.Channel.EuropaPlus || !radio.IsPlaying)
                    return false;
            }

            return true;
        }

        public RentResults FailRentResults => RentResults.RadioFail;
    }

    public class EngineSpecification : ICarSpecification<Car>
    {
        public bool IsSatisfied(Car car) => car.IsEngineOk();

        public RentResults FailRentResults => RentResults.EngineFail;
    }

    public class AirConditioningSpecification : ICarSpecification<Car>
    {
        public bool IsSatisfied(Car car)
        {
            var conditioner = car.AirConditioner;
            if (conditioner != null) {
                var temp = conditioner.CurrentTemperature;
                conditioner.TurnUp();
                if (conditioner.CurrentTemperature <= temp)
                    return false;
                conditioner.TurnDown();
                if (conditioner.CurrentTemperature != temp)
                    return false;
            }

            return true;
        }

        public RentResults FailRentResults => RentResults.AirConditioningFail;
    }
}