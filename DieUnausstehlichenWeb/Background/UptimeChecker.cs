using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using DieUnausstehlichenWeb.Services;
using Microsoft.Extensions.Hosting;

namespace DieUnausstehlichenWeb.Background
{
    public class UptimeChecker : IHostedService
    {
        private readonly IUptimeProvider _provider;
        private Timer _timer;

        public UptimeChecker(IUptimeProvider provider)
        {
            _provider = provider;
        }

        private async Task DoWork()
        {
            await _provider.CheckUpstate();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ => await DoWork(), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}