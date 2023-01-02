using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public static class DoctorMapper
    {
        public static Doctor Map(System.Data.SQLite.SQLiteDataReader reader)
        {
            Doctor doctor = new();

            doctor.Id = reader.GetInt32(0);
            doctor.Email = reader.GetString(1);
            doctor.OfficeNumber = reader.GetInt32(2);
            doctor.FirstName = reader.GetString(4);
            doctor.LastName = reader.GetString(5);
            doctor.Password = reader.GetString(6);
            doctor.Role = reader.GetString(7);

            return doctor;
        }
    }
}
