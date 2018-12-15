using PiApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PiApp.Services;

namespace PiApp.Server.Controllers
{
    [Route("api/[controller]")]
    public sealed class CameraController : ControllerBase
    {
        private readonly ICameraService _cameraService;

        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CaptureImage()
        {
            var imageBytes = await _cameraService.CaptureImageAsync();
            return File(imageBytes, "image/jpeg");
        }
    }
}
