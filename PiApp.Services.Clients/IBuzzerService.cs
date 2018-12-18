using System;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public interface IBuzzerService
    {
        Task BuzzAsync(TimeSpan time);

        Task StopAsync();
    }
}