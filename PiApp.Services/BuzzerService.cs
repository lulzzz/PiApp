using PiApp.Peripherals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;

namespace PiApp.Services
{
    public sealed class BuzzerService : IBuzzerService
    {
        private Buzzer _buzzer;

        public int ToneFrequency { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BuzzerService()
        {
            _buzzer = new Buzzer(Pi.Gpio.Pin27);
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
