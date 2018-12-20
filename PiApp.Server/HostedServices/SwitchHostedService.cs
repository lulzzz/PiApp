using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PiApp.Server.Hubs;
using PiApp.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiApp.Server.HostedServices
{
    public sealed class SwitchHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IHubContext<SwitchHub> _switchHubContext;
        private readonly ISwitchService _switchService;

        public SwitchHostedService(
            ILogger<SwitchHostedService> logger,
            ISwitchService switchService,
            IHubContext<SwitchHub> switchHubContext)
        {
            _logger = logger;
            _switchHubContext = switchHubContext;
            _switchService = switchService;
        }

        public void Dispose()
        {
            _switchService.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _switchService.Open += Open;
            _switchService.Closed += Closed;

            return Task.CompletedTask;
        }

        private void Open(object sender, EventArgs e)
        {
            _switchHubContext.Clients.All.SendAsync("Open", string.Empty);
        }

        private void Closed(object sender, EventArgs e)
        {
            _switchHubContext.Clients.All.SendAsync("Closed", string.Empty);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _switchService.Open -= Open;
            _switchService.Closed -= Closed;

            return Task.CompletedTask;
        }
    }
}
