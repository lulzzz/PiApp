using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PiApp.Services;

namespace PiApp.Server.Controllers
{
    [Route("api/[controller]")]
    public sealed class SwitchController : ControllerBase
    {
        private readonly ISwitchService _switchService;

        public SwitchController(ISwitchService switchService)
        {
            _switchService = switchService;
        }

        [HttpGet]
        public async Task<bool> GetState()
        {
            return await _switchService.GetStateAsync();
        }
    }
}
