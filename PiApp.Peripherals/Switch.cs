using System;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Gpio;
using Unosquare.RaspberryIO.Native;

namespace PiApp.Peripherals
{
    public sealed class Switch : ISwitch
    {
        private ulong _pressedLastInterrupt;
        private ulong _releasedLastInterrupt;

        /// <summary>
        /// Initializes a new instance of the <see cref="Switch"/> class.
        /// </summary>
        /// <param name="pin">The gpio pin.</param>
        public Switch(
            GpioPin pin,
            ulong interruptTime = 100)
        {
            Pin = pin;

            Pin.InputPullMode = GpioPinResistorPullMode.PullDown;
            Pin.PinMode = GpioPinDriveMode.Input;
            Pin.RegisterInterruptCallback(EdgeDetection.RisingAndFallingEdges, HandleInterrupt);

            InterruptTime = interruptTime;
        }

        public GpioPin Pin { get; }

        public ulong InterruptTime { get; }

        /// <summary>
        /// Occurs when [closed].
        /// </summary>
        public event EventHandler<EventArgs> Closed;

        /// <summary>
        /// Occurs when [open].
        /// </summary>
        public event EventHandler<EventArgs> Open;

        private void HandleInterrupt()
        {
            if (Pin.Read())
            {
                HandleCircuitClosed();
            }
            else
            {
                HandleCircuitOpen();
            }
        }

        private void HandleCircuitClosed()
        {
            ulong interruptTime = WiringPi.Millis();

            if (interruptTime - _pressedLastInterrupt <= InterruptTime) return;
            _pressedLastInterrupt = interruptTime;
            Closed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleCircuitOpen()
        {
            ulong interruptTime = WiringPi.Millis();

            if (interruptTime - _releasedLastInterrupt <= InterruptTime) return;
            _releasedLastInterrupt = interruptTime;
            Open?.Invoke(this, EventArgs.Empty);
        }

        public async Task<bool> GetStateAsync()
        {
            return await Pin.ReadAsync();
        }
    }
}
