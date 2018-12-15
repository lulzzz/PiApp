using System.Collections.Generic;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface IRelayService
    {
        IEnumerable<int> GetRelays();
        Task<bool> GetRelayStateAsync(int relay);
        Task SetRelayStateAsync(int relay, bool value);
    }
}