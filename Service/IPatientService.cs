using ZdravotniSystem.Controllers;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.Model;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IPatientService
    {
        void registerPatient(RegistrationModel model);
    }

    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly PatientRepository repository;

        public PatientService(
            ILogger<PatientService> logger, 
            PatientRepository repository
        ) {
            _logger = logger;
            this.repository = repository;
        }

        public void registerPatient(RegistrationModel model) {
            Patient patient = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,

            };
            _userRepository.Save(patient, "PATIENT");
            repository.Save(patient);
        }
    }
}
