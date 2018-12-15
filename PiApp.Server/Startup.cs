using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using PiApp.Services;
using PiApp.Server.HostedServices;
using PiApp.Server.Hubs;
using System.Linq;
using System.Net.Mime;

namespace PiApp.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR();

            services.AddSingleton<IRfidReader, RfidReader>();
            services.AddSingleton<ILEDService, LEDService>();
            services.AddSingleton<ICameraService, CameraService>();
            services.AddSingleton<IRelayService, RelayService>();

            services.AddHostedService<RfidReaderHostedService>();
            services.AddHostedService<CallButtonHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}"));

            app.UseSignalR(route =>
            {
                route.MapHub<RfidReaderHub>("/rfid-reader");
                route.MapHub<CallButtonHub>("/call-button");
                route.MapHub<RelayHub>("/relay-hub");
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
