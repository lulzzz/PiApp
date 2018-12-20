using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using PiApp.Services;
using System.Collections.Generic;
using PiApp.Shared;

namespace PiApp.Server.Controllers
{
    [Route("api/[controller]")]
    public sealed class GpioController : ControllerBase
    {
        private readonly IGpioService _gpioService;

        public GpioController(IGpioService gpioService)
        {
            _gpioService = gpioService;
        }

        [HttpGet]
        public async Task<IEnumerable<GpioPinInfo>> GetGpioPins()
        {
            return await _gpioService.GetGpioPinsAsync();
        }
    }
}
