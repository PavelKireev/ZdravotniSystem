using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.Service;

namespace ZdravotniSystem.Controllers
{
    [Route("api/appointment")]
    [ApiController, Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("list")]
        public List<Appointment> Get()
        {
            return _appointmentService.FindAll();
        }

        [HttpPost("create")]
        public void Post([FromBody] Appointment value)
        {
            _appointmentService.Create(value);
        }

        [HttpDelete("delete")]
        public void Delete(int id)
        {
            _appointmentService.Delete(id);
        }
    }
}
