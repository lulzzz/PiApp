using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface ICameraService
    {
        Task<byte[]> CaptureImageAsync();
    }
}