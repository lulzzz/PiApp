using System;

namespace PiApp.Peripherals
{
    public interface IButton
    {
        event EventHandler<EventArgs> Pressed;
        event EventHandler<EventArgs> Released;
    }
}