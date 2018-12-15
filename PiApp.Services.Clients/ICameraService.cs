using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public interface ICameraService
    {
        Task<string> CaptureImageAsync();
    }
}