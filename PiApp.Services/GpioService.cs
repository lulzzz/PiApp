using PiApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace PiApp.Services
{
    public sealed class GpioService : IGpioService
    {
        private GpioController _gpioController;

        public GpioService()
        {
            _gpioController = Pi.Gpio;
        }

        public Task<IEnumerable<GpioPinInfo>> GetGpioPinsAsync()
        {
            return Task.FromResult(
                _gpioController.Select(ToGpioPinInfo));
        }

        private static GpioPinInfo ToGpioPinInfo(GpioPin pin)
        {
            return new GpioPinInfo {
                HeaderPinNumber = pin.HeaderPinNumber,
                BcmPinNumber = pin.BcmPinNumber,
                WiringPiPinNumber = (int)pin.WiringPiPinNumber,
                PinMode = (GpioPinMode)pin.PinMode,
                Value = pin.Read(),
            };
        }
    }
}
