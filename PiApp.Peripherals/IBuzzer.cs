using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiApp.Peripherals
{
    public interface IBuzzer
    {
        int ToneFrequency { get; set; }
        Task BuzzAsync(CancellationToken cancellationToken = default);
        Task BuzzAsync(TimeSpan time, CancellationToken cancellationToken = default);
        void Stop();
    }
}