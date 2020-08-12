using KneatSC.Presenters;
using KneatSC.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KneatSC
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMainPresenter, MainPresenter>();
            services.AddSingleton<IStarshipService, StarshipService>();
            services.AddSingleton<Program, Program>();
        }

        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder();
            
            ConfigureServices(services);
            Configuration = builder.Build();

            var provider = services.BuildServiceProvider();

            var ctSource = new CancellationTokenSource();

            Task task = Task.Run(async () =>
            {
                Program program = provider.GetRequiredService<Program>();

                await program.Run(ctSource.Token);
            });

            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }

            ctSource.Cancel();
            ctSource.Dispose();
        }
    }
}
