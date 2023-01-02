using ZdravotniSystem.DB.Entity;

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
        public void Create(WorkingHours workingHours)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<WorkingHours> GetWorkingHoursByDoctorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
