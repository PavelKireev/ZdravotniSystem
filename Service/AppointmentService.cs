using ZdravotniSystem.Controllers;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.Repository;

namespace ZdravotniSystem.Service
{
    public interface IAppointmentService
    {
        List<Appointment> FindAll();
        List<Appointment> FindAllByUserIdAndRole(int userId, string role);
        List<Appointment> FindAllByDoctorId(int doctorId);
        void Delete(int id);
        void Create(Appointment appointment);
    }

    public class AppointmentService : IAppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;
        private readonly IAppointmentRepository _appointmetRepository;

        public AppointmentService(ILogger<AppointmentService> logger, IAppointmentRepository appointmetRepository)
        {
            _logger = logger;
            _appointmetRepository = appointmetRepository;
        }

        public void Create(Appointment appointment)
        {
            _appointmetRepository.Save(appointment);
        }

        public void Delete(int id)
        {
            _appointmetRepository.Delete(id);
        }

        public List<Appointment> FindAll()
        {
            return _appointmetRepository.FindAll();
        }

        public List<Appointment> FindAllByDoctorId(int doctorId)
        {
            return _appointmetRepository.FindAll();
        }

        public List<Appointment> FindAllByUserIdAndRole(int userId, string role)
        {
            throw new NotImplementedException();
        }
    }

}
