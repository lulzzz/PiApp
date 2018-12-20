using System.Collections.Generic;
using System.Threading.Tasks;
using PiApp.Shared;

namespace PiApp.Services
{
    public interface IGpioService
    {
        Task<IEnumerable<GpioPinInfo>> GetGpioPinsAsync();
    }
}