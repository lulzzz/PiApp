using System.Collections.Generic;
using System.Threading.Tasks;
using PiApp.Shared;

namespace PiApp.Services.Clients
{
    public interface IGpioService
    {
        Task<IEnumerable<GpioPinInfo>> GetGpioPinsAsync();
    }
}