﻿using System;
using Unosquare.RaspberryIO.Gpio;
using Unosquare.RaspberryIO.Native;

namespace PiApp.Peripherals
{
    /// <summary>
    /// Implements a generic button attached to the GPIO.
    /// </summary>
    public sealed class Button : IButton
    {
        private ulong _pressedLastInterrupt;
        private ulong _releasedLastInterrupt;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="gpioPin">The gpio pin.</param>
        public Button(GpioPin gpioPin, ulong interruptTime = 100)
        {
            Pin = gpioPin;

            Pin.InputPullMode = GpioPinResistorPullMode.PullDown;
            Pin.PinMode = GpioPinDriveMode.Input;
            Pin.RegisterInterruptCallback(EdgeDetection.RisingAndFallingEdges, HandleInterrupt);

            InterruptTime = interruptTime;
        }

        public GpioPin Pin { get; }

        public ulong InterruptTime { get; }

        /// <summary>
        /// Occurs when [pressed].
        /// </summary>
        public event EventHandler<EventArgs> Pressed;

        /// <summary>
        /// Occurs when [released].
        /// </summary>
        public event EventHandler<EventArgs> Released;

        private void HandleInterrupt()
        {
            if (Pin.Read())
            {
                HandleButtonPressed();
            }
            else
            {
                HandleButtonReleased();
            }
        }

        private void HandleButtonPressed()
        {
            ulong interruptTime = WiringPi.Millis();

            if (interruptTime - _pressedLastInterrupt <= InterruptTime) return;
            _pressedLastInterrupt = interruptTime;
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleButtonReleased()
        {
            ulong interruptTime = WiringPi.Millis();

            if (interruptTime - _releasedLastInterrupt <= InterruptTime) return;
            _releasedLastInterrupt = interruptTime;
            Released?.Invoke(this, EventArgs.Empty);
        }
    }
}
