using System;
using System.Threading.Tasks;

namespace PiApp.Peripherals
{
    public interface ISwitch
    {
        ulong InterruptTime { get; }

        event EventHandler<EventArgs> Closed;
        event EventHandler<EventArgs> Open;

        Task<bool> GetStateAsync();
    }
}