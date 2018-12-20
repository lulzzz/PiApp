using Newtonsoft.Json;
using PiApp.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class RelayControlService : IRelayControlService
    {
        private readonly HttpClient _client;

        public RelayControlService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<RelayInfo>> GetRelaysAsync()
        {
            var response = await _client.GetStringAsync($"/api/Relays");
            return JsonConvert.DeserializeObject<IEnumerable<RelayInfo>> (response);
        }

        public async Task<bool> GetRelayStateAsync(int relay)
        {
            var response = await _client.GetStringAsync($"/api/Relays/{relay}");
            return bool.Parse(response);
        }

        public async Task SetRelayStateAsync(int relay, bool state)
        {
            await _client.PostAsync($"/api/Relays/{relay}", new StringContent(state.ToString().ToLower(), Encoding.UTF8, "application/json"));
        }
    }
}
