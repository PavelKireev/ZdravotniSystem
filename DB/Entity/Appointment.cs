namespace ZdravotniSystem.DB.Entity
{
    public class Appointment : BaseEntity
    {
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public string Time { get; set; }

    }
}
