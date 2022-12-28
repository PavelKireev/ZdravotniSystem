using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZdravotniSystem.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorRestController : ControllerBase
    {

        private readonly IDoctorService _doctorService;

        public DoctorRestController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;
        }

        // GET: api/<DoctorRestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DoctorRestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorRestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoctorRestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorRestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
