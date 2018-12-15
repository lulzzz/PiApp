using PiApp.Shared;
using System;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface IRfidReader
    {
        Task StartAsync();
        Task StopAsync();
        Task<CardData> ReadCardUniqueIdAsync();
        IObservable<CardData> WhenCardDetected { get; }
    }
}
