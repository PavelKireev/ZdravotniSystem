using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.Model;
using ZdravotniSystem.Service;

namespace ZdravotniSystem.Controller
{
    [Route("api/working-hours")]
    [ApiController, Authorize]
    public class WorkingHoursController : ControllerBase
    {
        public readonly IWorkingHoursService _workingHoursService;

        public WorkingHoursController(IWorkingHoursService workingHoursService)
        {
            _workingHoursService = workingHoursService;
        }

        [HttpGet("list")]
        public List<WorkingHours> Get(
            int doctorId
        ) {
            return _workingHoursService.GetWorkingHoursByDoctorId(doctorId);
        }

        [HttpPost("create")]
        public void Post([FromBody] WorkingHours value)
        {
            _workingHoursService.Create(value);
        }

        [HttpDelete("delete")]
        public void Delete(int id)
        {
            _workingHoursService.Delete(id);
        }
    }
}
