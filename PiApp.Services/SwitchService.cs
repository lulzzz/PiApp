using Microsoft.Extensions.Logging;
using PiApp.Peripherals;
using System;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;

namespace PiApp.Services
{
    public sealed class SwitchService : ISwitchService
    {
        private readonly ILogger<SwitchService> _logger;
        private readonly ISwitch _switch;

        public SwitchService(ILogger<SwitchService> logger)
        {
            _logger = logger;
            _switch = new Switch(Pi.Gpio.Pin25);
            _switch.Open += (s, e) => Open?.Invoke(s, e);
            _switch.Closed += (s, e) => Closed?.Invoke(s, e);
        }

        public event EventHandler<EventArgs> Open;

        public event EventHandler<EventArgs> Closed;

        public void Dispose()
        {

        }

        public Task<bool> GetStateAsync()
        {
            return _switch.GetStateAsync();
        }
    }
}
