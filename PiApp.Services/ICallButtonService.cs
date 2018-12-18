using System;

namespace PiApp.Services
{
    public interface ICallButtonService : IDisposable
    {
        event EventHandler<EventArgs> Pressed;
        event EventHandler<EventArgs> Released;
    }
}