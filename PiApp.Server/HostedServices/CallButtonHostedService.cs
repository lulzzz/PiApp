using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PiApp.Server.Hubs;
using PiApp.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Peripherals;

namespace PiApp.Server.HostedServices
{
    public sealed class CallButtonHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Button button;
        private readonly IHubContext<CallButtonHub> _callButtonHubContext;
        private readonly ICallButtonService _callButtonService;

        public CallButtonHostedService(
            ILogger<CallButtonHostedService> logger,
            ICallButtonService callButtonService,
            IHubContext<CallButtonHub> callButtonHubContext)
        {
            _logger = logger;
            _callButtonHubContext = callButtonHubContext;
            _callButtonService = callButtonService;
        }

        public void Dispose()
        {
            _callButtonService.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _callButtonService.Pressed += Button_Pressed;
            _callButtonService.Released += Button_Released;

            return Task.CompletedTask;
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            _callButtonHubContext.Clients.All.SendAsync("ButtonPressed", string.Empty);
        }

        private void Button_Released(object sender, EventArgs e)
        {
            _callButtonHubContext.Clients.All.SendAsync("ButtonReleased", string.Empty);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _callButtonService.Pressed -= Button_Pressed;
            _callButtonService.Released -= Button_Released;

            return Task.CompletedTask;
        }
    }
}
