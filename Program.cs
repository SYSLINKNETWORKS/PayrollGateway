using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMLib.SwaggerForOcelot.DependencyInjection;

namespace TWP_API_GW {
    public class Program {
        public static void Main (string[] args) {
            CreateHostBuilder (args).Build ().Run ();
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            // Host.CreateDefaultBuilder(args)
            //     .ConfigureWebHostDefaults(webBuilder =>
            //     {
            //         webBuilder.UseStartup<Startup>();
            //     });
            Host.CreateDefaultBuilder (args)
            .ConfigureAppConfiguration ((hostingContext, config) => {
                config
                    .SetBasePath (hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
                    .AddJsonFile ($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                        optional : true, reloadOnChange : true)
                    .AddJsonFile ($"appsettings.local.json", optional : true, reloadOnChange : true)
                    .AddOcelotWithSwaggerSupport ((o) => {
                        o.Folder = "Configuration";
                    })
                    .AddEnvironmentVariables ();
            })
            .ConfigureWebHostDefaults (webBuilder => webBuilder.UseStartup<Startup> ());
    }
}