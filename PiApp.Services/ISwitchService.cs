using System;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface ISwitchService : IDisposable
    {
        event EventHandler<EventArgs> Closed;
        event EventHandler<EventArgs> Open;

        Task<bool> GetStateAsync();
    }
}