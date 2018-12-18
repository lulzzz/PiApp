using PiApp.Peripherals;
using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO;

namespace PiApp.Services
{
    public sealed class CallButtonService : ICallButtonService
    {
        private Button button;

        public CallButtonService()
        {
            button = new Button(Pi.Gpio.Pin03);
            button.Pressed += Button_Pressed;
            button.Released += Button_Released;
        }

        /// <summary>
        /// Occurs when [pressed].
        /// </summary>
        public event EventHandler<EventArgs> Pressed;

        /// <summary>
        /// Occurs when [released].
        /// </summary>
        public event EventHandler<EventArgs> Released;

        private void Button_Released(object sender, EventArgs e)
        {
            Released?.Invoke(this, e);
        }

        private void Button_Pressed(object sender, EventArgs e)
        {
            Pressed?.Invoke(this, e);
        }

        public void Dispose()
        {
            button.Pressed -= Button_Pressed;
            button.Released -= Button_Released;
            button = null;
        }
    }
}
