using ElectronNET.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Entelect.TimesheetsToHollardJira.Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) { 
        
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IHostingEnvironment env = builderContext.HostingEnvironment;

                    config
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .UseElectron(args)
                .UseStartup<Startup>();
        }
    }
}
