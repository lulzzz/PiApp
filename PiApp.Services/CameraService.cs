using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unosquare.RaspberryIO.Camera;

namespace PiApp.Services
{
    public sealed class CameraService : IDisposable, ICameraService
    {
        private readonly ILogger _logger;
        private readonly CameraController _camera;

        public CameraService(ILogger<CameraService> logger)
        {
            _logger = logger;
            _camera = Unosquare.RaspberryIO.Pi.Camera;
        }

        public async Task<byte[]> CaptureImageAsync()
        {
            return await _camera.CaptureImageAsync(new CameraStillSettings()).ConfigureAwait(false);
        }

        public void Dispose()
        {
        }
    }
}
