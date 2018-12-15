using PiApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiApp.Services;

namespace PiApp.Server.Controllers
{
    [Route("api/[controller]")]
    public sealed class LEDController : ControllerBase
    {
        private readonly ILEDService _ledService;

        public LEDController(ILEDService ledService)
        {
            _ledService = ledService;
        }

        [HttpPost("[action]")]
        public async Task Toggle()
        {
            await _ledService.ToggleAsync();
        }

        [HttpPost("[action]")]
        public async Task Set(bool state)
        {
            await _ledService.TurnOnAsync();
        }
    }
}
