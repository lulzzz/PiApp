using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface IRelayService : IDisposable
    {
        IEnumerable<int> GetRelays();
        Task<bool> GetRelayStateAsync(int relay);
        Task SetRelayStateAsync(int relay, bool value);
    }
}