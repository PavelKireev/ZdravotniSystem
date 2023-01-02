using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DTO;
using ZdravotniSystem.Model;
using ZdravotniSystem.Service;

namespace ZdravotniSystem.Controller
{
    [Route("api/user")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public UserController(IDoctorService doctorService, IPatientService patientService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpPost("create")]
        public void Create([FromBody] UserCreateDto dto)
        {
            if (dto != null && dto.Role.Equals("DOCTOR"))
            {
                _doctorService.Create(
                    new Doctor()
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        OfficeNumber = dto.OfficeNumber,
                        Password = dto.Password
                    });
            }
            else
            {
                _patientService.RegisterPatient(
                    new RegistrationModel()
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        PhoneNumber = dto.PhoneNumber,
                        InsuranceNumber = dto.InsuranceNumber,
                        BirthDate = dto.BirthDate,
                        Password = dto.Password
                    });
            }
        }
    }
}
