using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public sealed class CameraService : ICameraService
    {
        private readonly HttpClient _client;

        public CameraService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> CaptureImageAsync()
        {
            var response = await _client.PostAsync("/api/Camera/CaptureImage", new StringContent(string.Empty));
            var imageBytes = await response.Content.ReadAsByteArrayAsync();
            return Convert.ToBase64String(imageBytes);
        }
    }
}
