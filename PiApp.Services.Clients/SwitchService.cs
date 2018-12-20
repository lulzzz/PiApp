using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class SwitchService : IDisposable, ISwitchService
    {
        private readonly HubConnection _hubConnection;
        private readonly ILogger<SwitchService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IDisposable closedSubscription;
        private readonly IDisposable openSubscription;

        public SwitchService(ILogger<SwitchService> logger, HttpClient httpClient, HubConnection hubConnection)
        {
            _logger = logger;
            _httpClient = httpClient;
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

            closedSubscription = _hubConnection.On<string>("Closed", _ =>
            {
                Closed?.Invoke(this, EventArgs.Empty);
            });

            openSubscription = _hubConnection.On<string>("Open", _ =>
            {
                Open?.Invoke(this, EventArgs.Empty);
            });
        }

        public async Task<bool> GetStateAsync()
        {
            return JsonConvert.DeserializeObject<bool>(
                await _httpClient.GetStringAsync("/api/Switch"));
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
            closedSubscription.Dispose();
            openSubscription.Dispose();
        }

        public event EventHandler Closed;
        public event EventHandler Open;
    }
}
