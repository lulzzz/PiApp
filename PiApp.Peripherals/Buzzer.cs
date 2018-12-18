using System;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Gpio;
using Unosquare.RaspberryIO.Native;

namespace PiApp.Peripherals
{
    public sealed class Buzzer
    {
        private GpioPin _pin;
        private CancellationTokenSource cts;

        public Buzzer(GpioPin pin)
        {
            _pin = pin;
            _pin.PinMode = GpioPinDriveMode.Output;
        }

        public int ToneFrequency { get; set; } = 523;

        public async Task BuzzAsync(CancellationToken cancellationToken = default)
        {
            await BuzzAsync(Timeout.InfiniteTimeSpan, cancellationToken);
        }

        public async Task BuzzAsync(TimeSpan time, CancellationToken cancellationToken = default)
        {
            cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            _pin.SoftToneFrequency = ToneFrequency;
            try
            {
                await Task.Delay(time, cts.Token);
            }
            catch (TaskCanceledException)
            {
                // Just ignore. It's OK.
            }
            finally
            {
                _pin.SoftToneFrequency = 0;
                cts = null;
            }
        }

        public void Stop()
        {
            cts?.Cancel();
        }
    }
}
