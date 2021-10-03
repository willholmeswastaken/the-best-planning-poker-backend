using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TBPP.API.Session;
using TBPP.API.Session.Models;

namespace TBPP.API
{
    public static class AkkaExtensions
    {
        public static void AddAkka(this IServiceCollection services)
        {
            services.AddSingleton<IPokerRoomSessionHandler, AkkaService>();
            services.AddHostedService(sp => (AkkaService)sp.GetRequiredService<IPokerRoomSessionHandler>());
        }
    }

    public sealed class AkkaService : IHostedService, IPokerRoomSessionHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger<AkkaService> _logger;
        private ActorSystem _system;

        public AkkaService(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime, ILogger<AkkaService> logger)
        {
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = applicationLifetime;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("*******************");
            _logger.LogInformation("Started AkkaService");
            _logger.LogInformation("*******************");

            var serviceProviderSetup = ServiceProviderSetup.Create(_serviceProvider);

            var bootstrapSetup = BootstrapSetup.Create();
            _system = ActorSystem.Create("TbppSys", serviceProviderSetup.And(bootstrapSetup));

            _system.WhenTerminated.ContinueWith(tr => _hostApplicationLifetime.StopApplication());

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _system.Terminate();
        }

        public void Handle(IPokerRoomSessionMessage message)
        {
            
        }
    }
}
