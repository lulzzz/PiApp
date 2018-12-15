using System.Collections.Generic;
using System.Threading.Tasks;
using PiApp.Shared;

namespace PiApp.Services.Clients
{
    public interface IRelayControlService
    {
        Task<IEnumerable<RelayInfo>> GetRelaysAsync();
        Task<bool> GetRelayStateAsync(int relay);
        Task SetRelayStateAsync(int relay, bool state);
    }
}