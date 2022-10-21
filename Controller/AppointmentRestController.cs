using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using ZdravotniSystem.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZdravotniSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentRestController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentRestController(IAppointmentService appointmentService)
        {
            this._appointmentService = appointmentService;
        }


        // GET: api/<AppointmentRestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppointmentRestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AppointmentRestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AppointmentRestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AppointmentRestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
