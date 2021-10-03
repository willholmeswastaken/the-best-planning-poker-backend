using Akka.Actor;
using Akka.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TBPP.API
{
    public static class AkkaExtensions
    {
        public static void AddAkka(this IServiceCollection services)
        {
            // add akka stuff
        }
    }
    public class AkkaService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private ActorSystem _system;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceProviderSetup = ServiceProviderSetup.Create(_serviceProvider);

            var bootstrapSetup = BootstrapSetup.Create();
            _system = ActorSystem.Create("TbppSys", serviceProviderSetup.And(bootstrapSetup));

            _system.WhenTerminated.ContinueWith(tr => _hostApplicationLifetime.StopApplication());

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
