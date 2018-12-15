using PiApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiApp.Services;
using Microsoft.AspNetCore.SignalR;
using PiApp.Server.Hubs;

namespace PiApp.Server.Controllers
{
    [Route("api/[controller]")]
    public sealed class RelaysController : ControllerBase
    {
        private readonly IRelayService _relayService;
        private readonly IHubContext<RelayHub> _relayHubContext;

        public RelaysController(IRelayService relayService, IHubContext<RelayHub> relayHubContext)
        {
            _relayService = relayService;
            _relayHubContext = relayHubContext;
        }

        [HttpGet]
        public IEnumerable<RelayInfo> GetRelays()
        {
            var states = _relayService.GetRelays();
            return states
                .Select(async relay => (relay, await _relayService.GetRelayStateAsync(relay)))
                .Select(t => t.Result)
                .Select(t => new RelayInfo(t.relay, t.Item2));
        }

        [HttpGet("{relay}")]
        public async Task<bool> GetRelayState(int relay)
        {
            return await _relayService.GetRelayStateAsync(relay);
        }

        [HttpPost("{relay}")]
        public async Task SetRelayState(int relay, [FromBody] bool state)
        {
            await _relayService.SetRelayStateAsync(relay, state);
            await _relayHubContext.Clients.All.SendAsync("RelayStateSet", new RelayStateInfo(relay, state));
        }
    }
}
