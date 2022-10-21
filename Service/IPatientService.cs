using ZdravotniSystem.Controllers;

namespace ZdravotniSystem.Service
{
    public interface IPatientService
    {
    }

    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;

    }
}
