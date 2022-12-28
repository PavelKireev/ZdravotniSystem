using Microsoft.Extensions.Hosting;
using ZdravotniSystem.Configuration.Data;

namespace ZdravotniSystem.Configuration
{
    public class LifeTimeService : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            DataService.InitDbConnection();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            DataService.CloseDBConnection();
        }

    }

}
