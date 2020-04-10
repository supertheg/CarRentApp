namespace CarRentApp
{
    public class Technician
    {
        public static RentResults CheckMachineHealth(Car car)
        {
            if (!car.IsEngineOk())
                return RentResults.EngineFail;

            var radio = car.Radio;
            if (radio != null) {
                radio.On();
                if (!radio.IsRadioOn)
                    return RentResults.RadioFail;
                radio.SwitchChannel(Radio.Channel.EuropaPlus);
                if (radio.CurrentChannel != Radio.Channel.EuropaPlus || !radio.IsPlaying)
                    return RentResults.RadioFail;
            }

            var conditioner = car.AirConditioner;
            if (conditioner != null) {
                var temp = conditioner.CurrentTemperature;
                conditioner.TurnUp();
                if (conditioner.CurrentTemperature <= temp)
                    return RentResults.AirConditioningFail;
                conditioner.TurnDown();
                if (conditioner.CurrentTemperature != temp)
                    return RentResults.AirConditioningFail;
            }

            return RentResults.None;
        }
    }
}