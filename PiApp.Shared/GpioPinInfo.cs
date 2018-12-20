namespace PiApp.Shared
{
    public class GpioPinInfo
    {
        public int HeaderPinNumber { get; set; }
        public int BcmPinNumber { get; set; }
        public int WiringPiPinNumber { get; set; }
        public GpioPinMode PinMode { get; set; }
        public bool Value { get; set; }
    }

    public enum GpioPinMode
    {
        //
        // Summary:
        //     Input drive mode (perform reads)
        Input = 0,
        //
        // Summary:
        //     Output drive mode (perform writes)
        Output = 1,
        //
        // Summary:
        //     PWM output mode (only certain pins support this -- 2 of them at the moment)
        PwmOutput = 2,
        //
        // Summary:
        //     GPIO Clock output mode (only a pin supports this at this time)
        GpioClock = 3
    }
}