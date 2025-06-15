using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MultidrawingServer
{
    internal static class Program
    {
        static void Main()
        {
            Console.WriteLine("Multidrawing Server is starting...");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddGrpc();
                    services.AddSingleton<MultidrawingService.Service>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ListenAnyIP(5001, o => o.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGrpcService<MultidrawingService.Service>();
                        });
                    });
                })
                .Build();

            Console.WriteLine("Multidrawing Server is running...");
            host.Run();
        }
    }
}