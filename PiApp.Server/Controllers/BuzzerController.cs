using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using PiApp.Services;

namespace PiApp.Server.Controllers
{
    [Route("api/[controller]")]
    public sealed class BuzzerController : ControllerBase
    {
        private readonly IBuzzerService _buzzerService;

        public BuzzerController(IBuzzerService buzzerService)
        {
            _buzzerService = buzzerService;
        }

        [HttpPost("[action]")]
        public async Task Buzz([FromBody] int milliseconds = 5000)
        {
            await _buzzerService.BuzzAsync(TimeSpan.FromMilliseconds(milliseconds));
        }

        [HttpPost("[action]")]
        public void Stop()
        {
            _buzzerService.Stop();
        }
    }
}
