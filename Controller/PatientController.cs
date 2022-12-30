using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(string email)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put([FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
