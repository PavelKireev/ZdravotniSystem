using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Repository;

namespace ZdravotniSystem.Service
{

    public interface IWorkingHoursService
    {
        List<WorkingHours> GetWorkingHoursByDoctorId(int id);
        void Create(WorkingHours workingHours);
        void Delete(int id);
    }
    public class WorkingHoursService : IWorkingHoursService
    {
        private readonly IWorkingHoursRepository _workingHoursRepository;

        public WorkingHoursService(IWorkingHoursRepository workingHoursRepository)
        {
            _workingHoursRepository = workingHoursRepository;
        }

        public void Create(WorkingHours workingHours)
        {
            _workingHoursRepository.Save(workingHours);
        }

        public void Delete(int id)
        {
            _workingHoursRepository.Delete(id);
        }

        public List<WorkingHours> GetWorkingHoursByDoctorId(int id)
        {
            return _workingHoursRepository.FindAllByDoctorId(id);
        }
    }
}
