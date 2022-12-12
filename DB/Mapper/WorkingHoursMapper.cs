using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public static class WorkingHoursMapper
    {
        public static WorkingHours Map(System.Data.SQLite.SQLiteDataReader reader)
        {
            WorkingHours workingHours = new();

            workingHours.Id = reader.GetInt32(0);
            workingHours.DayOfWeek = reader.GetString(1);
            workingHours.HourFrom = reader.GetInt32(2);
            workingHours.HoursCount = reader.GetInt32(3);
            workingHours.DoctorId = reader.GetInt32(4);

            return workingHours;
        }
    }
}
