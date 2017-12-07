using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace windforce_corp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) {

            return WebHost.CreateDefaultBuilder(args)
                // .ConfigureAppConfiguration((Context, config) => 
                    // config.AddJsonFile("appsetting.json"))
                .ConfigureLogging(loggerFactory => {
                    loggerFactory.AddConsole();
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}
