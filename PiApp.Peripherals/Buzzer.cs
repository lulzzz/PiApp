using System;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Gpio;

namespace PiApp.Peripherals
{
    public sealed class Buzzer : IBuzzer
    {
        private CancellationTokenSource cts;

        public Buzzer(GpioPin pin, int toneFrequency = 523)
        {
            Pin = pin;
            Pin.PinMode = GpioPinDriveMode.Output;

            ToneFrequency = toneFrequency;
        }

        public int ToneFrequency { get; set; } = 523;

        public GpioPin Pin { get; }

        public async Task BuzzAsync(CancellationToken cancellationToken = default)
        {
            await BuzzAsync(Timeout.InfiniteTimeSpan, cancellationToken);
        }

        public async Task BuzzAsync(TimeSpan time, CancellationToken cancellationToken = default)
        {
            cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            Pin.SoftToneFrequency = ToneFrequency;
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
                Pin.SoftToneFrequency = 0;
                cts = null;
            }
        }

        public void Stop()
        {
            cts?.Cancel();
        }
    }
}
