using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.Model;
using ZdravotniSystem.Service;

namespace ZdravotniSystem.Controllers
{
    [Route("api/doctor")]
    [ApiController, Authorize]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("list")]
        public List<Doctor> Get()
        {
            return _doctorService.FindAll();
        }

        [HttpGet]
        public DoctorModel Get(string email)
        {
            Doctor doctor = _doctorService.GetDoctorByEmail(email);

            return new DoctorModel()
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                OfficeNumber = doctor.OfficeNumber
            };
        }

        [HttpPost("update")]
        public void Post([FromBody] DoctorModel value)
        {
            _doctorService.Update(value);
        }

        [HttpDelete("delete")]
        public void Delete(string email)
        {
            _doctorService.DeleteByEmail(email);
        }
    }
}
