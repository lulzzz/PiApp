using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using PiApp.Shared;
using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class RfidReader : IRfidReader, IDisposable
    {
        private readonly Subject<CardData> _whenCardDataReceived;
        private readonly HubConnection _hubConnection;
        private readonly ILogger<RfidReader> _logger;
        private readonly IDisposable subscription;

        public RfidReader(ILogger<RfidReader> logger, HubConnection hubConnection)
        {
            _logger = logger;

            _whenCardDataReceived = new Subject<CardData>();

            _hubConnection = hubConnection;

            _hubConnection.Closed += async (exception) =>
            {
                if (exception != null)
                {
                    _logger.LogError(exception, "Connection unexpectedly.");

                    await Task.Delay(new Random().Next(0, 5) * 1000).ConfigureAwait(false);

                    try
                    {
                        await _hubConnection.StartAsync().ConfigureAwait(false);

                        _logger.LogInformation("Connection restarted.");
                    }
                    catch (Exception exc)
                    {
                        _logger.LogError(exc, "Failed to restart connection.");
                    }
                }
            };

            subscription = _hubConnection.On<CardData>("ReceiveCardData", cardData =>
            {
                _whenCardDataReceived.OnNext(cardData);
            });
        }

        public async Task StartAsync()
        {
            await _hubConnection.StartAsync().ConfigureAwait(false);

            _logger.LogInformation("Connection started.");
        }

        public async Task StopAsync()
        {
            await _hubConnection.StopAsync().ConfigureAwait(false);

            _logger.LogInformation("Connection stopped");
        }

        public void Dispose()
        {
            subscription.Dispose();
        }

        public IObservable<CardData> WhenCardDataReceived => _whenCardDataReceived;
    }
}
