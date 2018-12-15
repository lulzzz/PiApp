using System;
using System.Threading.Tasks;
using PiApp.Shared;

namespace PiApp.Services.Clients
{
    public interface IRfidReader
    {
        IObservable<CardData> WhenCardDataReceived { get; }

        Task StartAsync();
        Task StopAsync();
    }
}