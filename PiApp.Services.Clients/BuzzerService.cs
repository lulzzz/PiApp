using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class BuzzerService : IBuzzerService
    {
        private readonly HttpClient _client;

        public BuzzerService(HttpClient client)
        {
            _client = client;
        }

        public async Task BuzzAsync(TimeSpan time)
        {
            await _client.PostAsync("/api/Buzzer/Buzz", new StringContent(time.TotalMilliseconds.ToString(), Encoding.UTF8, "application/json"));
        }

        public async Task StopAsync()
        {
            await _client.PostAsync("/api/Buzzer/Stop", new StringContent(string.Empty));
        }
    }
}
