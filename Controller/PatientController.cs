using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.Model;
using ZdravotniSystem.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZdravotniSystem.Controllers
{
    [Route("api/patient")]
    [ApiController, Authorize]
    public class PatientController : ControllerBase
    {

        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }


        [HttpGet("list")]
        public List<Patient> Get()
        {
            return _patientService.GetPatients();
        }

        [HttpGet]
        public PatientModel Get(string email)
        {
            Patient patient = _patientService.GetPatientByEmail(email);
            DateTime birthDate = string.IsNullOrEmpty(patient.BirthDate) ? default : DateTime.Parse(patient.BirthDate);

            return new PatientModel()
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                BirthDate = birthDate,
                InsuranceNumber = patient.InsuranceNumber
            };
        }

        [HttpPost("update")]
        public void Post([FromBody] PatientModel value)
        {
            _patientService.Update(value);
        }

        [HttpDelete("delete")]
        public void Delete(string email)
        {
            _patientService.DeleteByEmail(email);
        }
    }
}
