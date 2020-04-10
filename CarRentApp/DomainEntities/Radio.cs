namespace CarRentApp
{
    public class Radio
    {
        public enum Channel
        {
            None,
            Energy,
            EuropaPlus,
            RadioRecord
        }

        public Channel CurrentChannel { get; private set; }

        public bool IsRadioOn { get; private set; }

        public bool IsPlaying => IsRadioOn && CurrentChannel != Channel.None;

        public void On() => IsRadioOn = true;

        public void Off()
        {
            IsRadioOn = false;
            CurrentChannel = Channel.None;
        }

        public void SwitchChannel(Channel channel)
        {
            if (IsRadioOn == false) return;
            CurrentChannel = channel;
        }
    }
}