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


        [HttpGet]
        public List<Patient> Get()
        {
            return _patientService.GetPatients();
        }

        [HttpGet("patient")]
        public Patient Get(string email)
        {
            return _patientService.GetPatientByEmail(email);
        }

        [HttpPost("update")]
        public void Post([FromBody] PatientModel value)
        {
        }

        [HttpDelete("delete")]
        public void Delete(string email)
        {

        }
    }
}
