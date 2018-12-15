using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface ILEDService
    {
        Task TurnOffAsync();
        Task TurnOnAsync();
        Task ToggleAsync();
    }
}