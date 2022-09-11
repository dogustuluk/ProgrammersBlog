using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();
                //çalýþma ortamýný alma
                var env = hostingContext.HostingEnvironment;
                //json dosyalarýný ekleme
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true,reloadOnChange:true);//appsettings.development.json
                //çalýþma ortamý deðiþkenlerini ekleme
                config.AddEnvironmentVariables();
                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(logging =>
                {/*
                  * NLog kullanmak istediðimiz için diðer provider'larý(logger'larý) deaktive et.
                  */
                    logging.ClearProviders();
                }).UseNLog();//MvcExceptionFilter içerisinde logger'ý kullan.
    }
}
