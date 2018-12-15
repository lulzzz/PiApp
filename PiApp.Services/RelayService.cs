using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace PiApp.Services
{
    public sealed class RelayService : IRelayService
    {
        private readonly GpioController _gpioController;
        private readonly IDictionary<int, GpioPin> _relayGpioMappings;

        public RelayService()
        {
            _gpioController = Pi.Gpio;

            _relayGpioMappings = new Dictionary<int, GpioPin> {
                { 0, _gpioController.Pin27 },
                { 1, _gpioController.Pin28 }
            };

            _gpioController.Pin27.PinMode = GpioPinDriveMode.Output;
            _gpioController.Pin28.PinMode = GpioPinDriveMode.Output;
        }

        public IEnumerable<int> GetRelays()
        {
            return _relayGpioMappings.Select(kp => kp.Key);
        }

        public async Task SetRelayStateAsync(int relay, bool value)
        {
            await _relayGpioMappings[relay].WriteAsync(!value);
        }

        public async Task<bool> GetRelayStateAsync(int relay)
        {
            return !(await _relayGpioMappings[relay].ReadAsync());
        }
    }
}
