using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface IBuzzerService : IDisposable
    {
        int ToneFrequency { get; set; }

        Task BuzzAsync(TimeSpan time, CancellationToken cancellationToken = default);

        void Stop();
    }
}