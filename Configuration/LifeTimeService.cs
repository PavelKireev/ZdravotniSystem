using Microsoft.Extensions.Hosting;
using ZdravotniSystem.Configuration.Data;

namespace ZdravotniSystem.Configuration
{
    public class LifeTimeService : IHostedService
    {
        private readonly DataService dataService;

        public LifeTimeService(DataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            dataService.InitDbConnection();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            DataService.CloseDBConnection();
        }

    }

}
