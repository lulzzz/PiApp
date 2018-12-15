using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PiApp.Services;
using PiApp.Server.Hubs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiApp.Server.HostedServices
{
    public sealed class RfidReaderHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IHubContext<RfidReaderHub> _rfidHubContext;
        private readonly IRfidReader _rfidReader;
        private IDisposable subscription;

        public RfidReaderHostedService(
            ILogger<RfidReaderHostedService> logger,
            IHubContext<RfidReaderHub> rfidHubContext,
            IRfidReader rfidReader)
        {
            _logger = logger;
            _rfidHubContext = rfidHubContext;

            _rfidReader = rfidReader;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RFID Service is starting.");

            subscription = _rfidReader.WhenCardDetected.Subscribe(async cardData =>
            {
                _logger.LogInformation("Sending UID");

                await _rfidHubContext.Clients.All.SendAsync("ReceiveCardData", cardData);
            });

            await _rfidReader.StartAsync();

            _logger.LogInformation("RFID Service has started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RFID Service is stopping.");

            await _rfidReader.StopAsync();

            _logger.LogInformation("RFID Service has stopped.");
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}
