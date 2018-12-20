using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class CallButtonService : IDisposable, ICallButtonService
    {
        private readonly HubConnection _hubConnection;
        private readonly ILogger<CallButtonService> _logger;
        private readonly IDisposable pressedSubscription;
        private readonly IDisposable releasedSubscription;

        public CallButtonService(ILogger<CallButtonService> logger, HubConnection hubConnection)
        {
            _logger = logger;

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

            pressedSubscription = _hubConnection.On<string>("ButtonPressed", _ =>
            {
                ButtonPressed?.Invoke(this, EventArgs.Empty);
            });

            releasedSubscription = _hubConnection.On<string>("ButtonReleased", _ =>
            {
                ButtonReleased?.Invoke(this, EventArgs.Empty);
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
            pressedSubscription?.Dispose();
            releasedSubscription?.Dispose();
        }

        public event EventHandler ButtonPressed;
        public event EventHandler ButtonReleased;
    }
}
