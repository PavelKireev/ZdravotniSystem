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

        private readonly IPatientRepository _repository;
        private readonly IUserRepository _userRepository;

        public PatientService(ILogger<PatientService> logger, IPatientRepository repository, IUserRepository userRepository)
        {
            _logger = logger;
            _repository = repository;
            _userRepository = userRepository;
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
            _repository.Save(patient);
        }
    }
}
