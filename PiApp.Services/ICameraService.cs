using System;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface ICameraService : IDisposable
    {
        Task<byte[]> CaptureImageAsync();
    }
}