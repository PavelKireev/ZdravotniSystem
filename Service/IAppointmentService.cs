using ZdravotniSystem.Controllers;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IAppointmentService
    {
        
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;
        private readonly IAppointmentRepository _appointmetRepository;

        public AppointmentService(ILogger<AppointmentService> logger, IAppointmentRepository appointmetRepository)
        {
            _logger = logger;
            _appointmetRepository = appointmetRepository;
        }
    }

}
