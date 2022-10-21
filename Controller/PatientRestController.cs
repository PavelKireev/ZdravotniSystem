using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZdravotniSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientRestController : ControllerBase
    {

        private readonly IPatientService _patientService;

        public PatientRestController(IPatientService patientService)
        {
            this._patientService = patientService;
        }


        // GET: api/<PatientRestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientRestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientRestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PatientRestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientRestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
