using System;
using System.Threading.Tasks;
using PiApp.Shared;

namespace PiApp.Services.Clients
{
    public interface IRelayNotifierService
    {
        IObservable<RelayStateInfo> WhenRelayStateSet { get; }

        void Dispose();
        Task StartAsync();
        Task StopAsync();
    }
}