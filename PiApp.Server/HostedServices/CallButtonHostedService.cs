using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PiApp.Server.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public CallButtonHostedService(
            ILogger<CallButtonHostedService> logger,
            IHubContext<CallButtonHub> callButtonHubContext)
        {
            _logger = logger;
            _callButtonHubContext = callButtonHubContext;
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            button = new Button(Pi.Gpio.Pin03);
            button.Pressed += Button_Pressed;
            button.Released += Button_Released;

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
            button.Pressed -= Button_Pressed;
            button.Released -= Button_Released;
            button = null;

            return Task.CompletedTask;
        }
    }
}
