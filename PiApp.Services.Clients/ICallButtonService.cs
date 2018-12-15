using System;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public interface ICallButtonService
    {
        event EventHandler ButtonPressed;
        event EventHandler ButtonReleased;

        Task StartAsync();
        Task StopAsync();
    }
}