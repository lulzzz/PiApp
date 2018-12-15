using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using PiApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class RelayNotifierService : IDisposable, IRelayNotifierService
    {
        private readonly HubConnection _hubConnection;
        private readonly Subject<RelayStateInfo> _whenRelayStateSetSubject;
        private readonly ILogger<RelayNotifierService> _logger;
        private readonly IDisposable subscription;

        public IObservable<RelayStateInfo> WhenRelayStateSet => _whenRelayStateSetSubject;

        public RelayNotifierService(ILogger<RelayNotifierService> logger, HubConnection hubConnection)
        {
            _logger = logger;

            _hubConnection = hubConnection;

            _whenRelayStateSetSubject = new Subject<RelayStateInfo>();

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

            subscription = _hubConnection.On("RelayStateSet", (Action<RelayStateInfo>)(rsc =>
            {
                _whenRelayStateSetSubject.OnNext(rsc);
            }));
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
    }
}
