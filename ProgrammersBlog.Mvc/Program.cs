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
                //�al��ma ortam�n� alma
                var env = hostingContext.HostingEnvironment;
                //json dosyalar�n� ekleme
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true,reloadOnChange:true);//appsettings.development.json
                //�al��ma ortam� de�i�kenlerini ekleme
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
                  * NLog kullanmak istedi�imiz i�in di�er provider'lar�(logger'lar�) deaktive et.
                  */
                    logging.ClearProviders();
                }).UseNLog();//MvcExceptionFilter i�erisinde logger'� kullan.
    }
}
