using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace VegaIT.PremestiSE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration))
                .UseStartup<Startup>()
                .UseIIS()
                .CaptureStartupErrors(true)
                .Build();
    }
}
