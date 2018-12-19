using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace PiApp.Services
{
    public sealed class LEDService : ILEDService
    {
        private readonly ILogger _logger;
        private readonly GpioPin _pin;

        public LEDService(ILogger<LEDService> logger)
        {
            _logger = logger;

            _pin = Pi.Gpio.Pin24;
            _pin.PinMode = GpioPinDriveMode.Output;
        }

        public async Task TurnOnAsync()
        {
            await _pin.WriteAsync(true);
        }

        public async Task TurnOffAsync()
        {
            await _pin.WriteAsync(false);
        }

        public async Task ToggleAsync()
        {
            var isSet = await _pin.ReadAsync();
            await _pin.WriteAsync(!isSet);
        }

        public void Dispose()
        {

        }
    }
}
