using Autofac;
using Microsoft.Extensions.Logging;
using Surging.Core.Caching.Configurations;
using Surging.Core.Consul.Configurations;
using Surging.Core.CPlatform;
using Surging.Core.CPlatform.Configurations;
using Surging.Core.CPlatform.Utilities;
using Surging.Core.EventBusRabbitMQ.Configurations;
using Surging.Core.ProxyGenerator;
using Surging.Core.ServiceHosting;
using Surging.Core.ServiceHosting.Internal.Implementation;
using SurgingConfig = Surging.Core.CPlatform.AppConfig;
using System;
using Surging.Core.System.Intercept;
using Surging.Core.Zookeeper.Configurations;

namespace Surging.Hero.ServiceHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = new ServiceHostBuilder()
                 .RegisterServices(builder =>
                 {
                     builder.AddMicroService(option =>
                      {
                          option.AddServiceRuntime()
                           .AddClientProxy()
                           .AddRelateServiceRuntime()
                           .AddConfigurationWatch()
                           .AddServiceEngine(typeof(SurgingServiceEngine))
                           .AddClientIntercepted(typeof(CacheProviderInterceptor))
                           ;

                          builder.Register(p => new CPlatformContainer(ServiceLocator.Current));
                      });
                 })
                 .ConfigureLogging(loggging =>
                 {
                     loggging.AddConfiguration(
                         AppConfig.GetSection("Logging"));
                 })
                 .UseServer(options => { })
                 .UseConsoleLifetime()
                 .Configure(build =>
                 {
#if DEBUG
                     build.AddCacheFile("${cachePath}|/app/configs/cacheSettings.json", optional: false, reloadOnChange: true);
                     build.AddCPlatformFile("${surgingPath}|/app/configs/surgingSettings.json", optional: false, reloadOnChange: true);
                  //   build.AddEventBusFile("${eventBusPath}|/app/configs/eventBusSettings.json", optional: false);
                     build.AddConsulFile("${consulPath}|/app/configs/consul.json", optional: false, reloadOnChange: true);
                     build.AddZookeeperFile("${zookeeperPath}|/app/configs/zookeeper.json", optional: false, reloadOnChange: true);


#else
                    build.AddCacheFile("${cachePath}|configs/cacheSettings.json", optional: false, reloadOnChange: true);                      
                    build.AddCPlatformFile("${surgingPath}|configs/surgingSettings.json", optional: false,reloadOnChange: true);                    
                 //   build.AddEventBusFile("configs/eventBusSettings.json", optional: false);
                    build.AddConsulFile("configs/consul.json", optional: false, reloadOnChange: true);
                     build.AddZookeeperFile("${zookeeperPath}|configs/zookeeper.json", optional: false, reloadOnChange: true);
#endif
                 })
                 .UseProxy()                
                 .UseStartup<Startup>()
                 .Build(); 

            using (host.Run())
            {
                Console.WriteLine($"服务主机启动成功{DateTime.Now}。");

#if DEBUG
                //Startup.InitActions();
#endif
            }
        }
    }
}