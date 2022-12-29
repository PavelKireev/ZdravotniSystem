using ZdravotniSystem.Controllers;
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
        private readonly PatientRepository repository;

        public PatientService(
            ILogger<PatientService> logger, 
            PatientRepository repository
        ) {
            _logger = logger;
            this.repository = repository;
        }

        public void registerPatient(RegistrationModel model) {
            repository.Save(new DB.Entity.Patient
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email= model.Email
            });
        }
    }
}
