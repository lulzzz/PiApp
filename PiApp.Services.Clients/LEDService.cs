using System.Net.Http;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class LEDService : ILEDService
    {
        private readonly HttpClient _client;

        public LEDService(HttpClient client)
        {
            _client = client;
        }

        public async Task ToggleAsync()
        {
            await _client.PostAsync("/api/LED/Toggle", new StringContent(string.Empty));
        }
    }
}
