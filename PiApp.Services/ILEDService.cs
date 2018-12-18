using System;
using System.Threading.Tasks;

namespace PiApp.Services
{
    public interface ILEDService : IDisposable
    {
        Task TurnOffAsync();
        Task TurnOnAsync();
        Task ToggleAsync();
    }
}