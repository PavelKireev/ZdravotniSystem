using ZdravotniSystem.Controllers;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IDoctorService
    {
        void create(Doctor doctor);
    }

    public class DoctorService : IDoctorService
    {
        private readonly ILogger<DoctorService> _logger;
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(ILogger<DoctorService> logger, IDoctorRepository doctorRepository)
        {
            _logger = logger;
            _doctorRepository = doctorRepository;
        }

        public void create(Doctor doctor)
        {
            _doctorRepository.Save(doctor);
            _logger.LogInformation("Doctor {0} created", doctor.Email);
        }
    }
}
