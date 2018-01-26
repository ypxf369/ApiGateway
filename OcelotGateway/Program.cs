using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OcelotGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = new WebHostBuilder();

            //注入WebHostBuilder
            return builder.ConfigureServices(service =>
                {
                    service.AddSingleton(builder);
                })
                //加载configuration配置文件
                .ConfigureAppConfiguration(conbuilder =>
                {
                    conbuilder.AddJsonFile("configuration.json");
                })
                .UseKestrel()
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();
        }
    }
}
