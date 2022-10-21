using ZdravotniSystem.Controllers;

namespace ZdravotniSystem.Service
{
    public interface IDoctorService
    {
    }

    public class DoctorService : IDoctorService
    {
        private readonly ILogger<DoctorService> _logger;

    }
}
