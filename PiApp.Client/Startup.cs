using Blazor.Extensions.Logging;
using Blazor.Extensions.Storage;
using BlazorSignalR;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PiApp.Services.Clients;

namespace PiApp.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder
                    .AddBrowserConsole()
                    .SetMinimumLevel(LogLevel.Trace)
                );

            services.AddStorage();

            services.AddSingleton<IRfidReader>(sp =>
            {
                return new RfidReader(
                    sp.GetService<ILogger<RfidReader>>(),
                    new HubConnectionBuilder().WithUrlBlazor("/rfid-reader",
                            options: opt => opt.Transports = HttpTransportType.WebSockets).Build());
            });
            services.AddSingleton<ICallButtonService>(sp =>
            {
                return new CallButtonService(
                    sp.GetService<ILogger<CallButtonService>>(),
                    new HubConnectionBuilder().WithUrlBlazor("/call-button",
                            options: opt => opt.Transports = HttpTransportType.WebSockets).Build());
            });
            services.AddSingleton<ICameraService, CameraService>();
            services.AddSingleton<IRelayControlService, RelayControlService>();
            services.AddSingleton<IRelayNotifierService>(sp =>
            {
                return new RelayNotifierService(
                    sp.GetService<ILogger<RelayNotifierService>>(), 
                    new HubConnectionBuilder().WithUrlBlazor("/relay-hub",
                            options: opt => opt.Transports = HttpTransportType.WebSockets).Build());
            });

        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
