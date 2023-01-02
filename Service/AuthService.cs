using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;
using ZdravotniSystem.DTO;
using ZdravotniSystem.Model;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IAuthService
    {
        AuthUserDto GetAuthenticatedUser(string email, string role);
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public AuthService(
            IUserRepository userRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository
        ) {
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public AuthUserDto GetAuthenticatedUser(string email, string role)
        {
            switch (role)
            {
                case "ADMIN":
                    User user = _userRepository.GetOneByEmail(email);
                    return new AuthUserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                    };
                case "DOCTOR":
                    Doctor doctor = _doctorRepository.GetOneByEmail(email);
                    return new AuthUserDto()
                    {
                        Id = doctor.Id,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        Email = doctor.Email,
                        OfficeNumber = doctor.OfficeNumber 
                    };
                case "PATIENT":
                    Patient patient = _patientRepository.GetOneByEmail(email);
                    DateTime birthDate = string.IsNullOrEmpty(patient.BirthDate) ? default : DateTime.Parse(patient.BirthDate);
                    return new AuthUserDto()
                    {
                        Id = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        Email = patient.Email,
                        BirthDate = birthDate,
                        PhoneNumber = patient.PhoneNumber,
                        InsuranceNumber = patient.InsuranceNumber
                    };
                default: return new AuthUserDto();
            }
        }
    }
}
