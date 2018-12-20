using Newtonsoft.Json;
using PiApp.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class GpioService : IGpioService
    {
        private readonly HttpClient _client;

        public GpioService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<GpioPinInfo>> GetGpioPinsAsync()
        {
            var response = await _client.GetStringAsync($"/api/Gpio");
            return JsonConvert.DeserializeObject<IEnumerable<GpioPinInfo>>(response);
        }
    }
}
