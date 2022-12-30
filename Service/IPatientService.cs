using ZdravotniSystem.Controllers;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.Model;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IPatientService
    {
        void RegisterPatient(RegistrationModel model);
        List<Patient> GetPatients();
    }

    public class PatientService : IPatientService
    {
        private readonly ILogger<PatientService> _logger;

        private readonly IPatientRepository _repository;
        private readonly IUserRepository _userRepository;

        public PatientService(ILogger<PatientService> logger, IPatientRepository repository, IUserRepository userRepository)
        {
            _logger = logger;
            _repository = repository;
            _userRepository = userRepository;
        }

        public List<Patient> GetPatients()
        {
            return _repository.FindAll();
        }

        public void RegisterPatient(RegistrationModel model) {
            Patient patient = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,

            };
            _userRepository.Save(patient, "PATIENT");
            _repository.Save(patient);
            _logger.LogInformation("Patient {} successfully registered.", model.Email);
        }


    }
}
