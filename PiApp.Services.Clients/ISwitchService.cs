using System;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public interface ISwitchService
    {
        event EventHandler Closed;
        event EventHandler Open;

        Task StartAsync();
        Task StopAsync();
        Task<bool> GetStateAsync();
    }
}