namespace ZdravotniSystem.DB.Entity
{
    public class WorkingHours : BaseEntity
    {
        public string DayOfWeek { get; set; }
        public int HourFrom { get; set; }
        public int HoursCount { get; set; }
        public int DoctorId { get; set; }
    }
}
