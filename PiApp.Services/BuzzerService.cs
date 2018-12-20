using PiApp.Peripherals;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;

namespace PiApp.Services
{
    public sealed class BuzzerService : IBuzzerService
    {
        private readonly Buzzer _buzzer;

        public BuzzerService()
        {
            _buzzer = new Buzzer(Pi.Gpio.Pin23);
        }

        public async Task BuzzAsync(TimeSpan time, CancellationToken cancellationToken = default)
        {
            await _buzzer.BuzzAsync(time, cancellationToken);
        }

        public void Stop()
        {
            _buzzer.Stop();
        }

        public void Dispose()
        {
        }
    }
}
