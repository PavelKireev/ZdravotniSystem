using ZdravotniSystem.Controllers;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.Model;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IDoctorService
    {
        void Create(Doctor doctor);
        void DeleteByEmail(string email);
        List<Doctor> FindAll();
        Doctor GetDoctorByEmail(string email);
        void Update(DoctorModel value);
    }

    public class DoctorService : IDoctorService
    {
        private readonly ILogger<DoctorService> _logger;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserRepository _userRepository;

        public DoctorService(
            ILogger<DoctorService> logger, 
            IDoctorRepository doctorRepository, 
            IUserRepository userRepository
        ) {
            _logger = logger;
            _doctorRepository = doctorRepository;
            _userRepository = userRepository;
        }

        public void Create(Doctor doctor)
        {
            _userRepository.Save(doctor, "DOCTOR");
            _doctorRepository.Save(doctor);
            _logger.LogInformation("Doctor {0} created", doctor.Email);
        }

        public void DeleteByEmail(string email)
        {
            _userRepository.DeleteByEmail(email);
            _doctorRepository.DeleteByEmail(email);
        }

        public List<Doctor> FindAll()
        {
            return _doctorRepository.FindAll();
        }

        public Doctor GetDoctorByEmail(string email)
        {
            return _doctorRepository.GetOneByEmail(email);
        }

        public void Update(DoctorModel value)
        {
            Doctor doctor = new Doctor()
            {
                Id = value.Id,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                OfficeNumber = value.OfficeNumber
            };
            _userRepository.Update(doctor, "DOCTOR");
            _doctorRepository.Update(doctor);
        }
    }
}
