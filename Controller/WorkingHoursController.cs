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
        private readonly IWorkingHoursService _workingHoursService;

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
        public List<WorkingHours> Post([FromBody] WorkingHours value, int doctorId)
        {
            value.DoctorId = doctorId;
            _workingHoursService.Create(value);
            return _workingHoursService.GetWorkingHoursByDoctorId(doctorId);
        }

        [HttpDelete("delete")]
        public List<WorkingHours> Delete(int id, int doctorId)
        {
            _workingHoursService.Delete(id);
            return _workingHoursService.GetWorkingHoursByDoctorId(doctorId);
        }
    }
}
