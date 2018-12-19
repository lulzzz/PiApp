using System.Threading.Tasks;

namespace PiApp.Services.Clients
{
    public interface ILEDService
    {
        Task ToggleAsync();
    }
}