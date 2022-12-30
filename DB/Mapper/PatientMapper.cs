using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public static class PatientMapper
    {
        public static Patient Map(System.Data.SQLite.SQLiteDataReader reader)
        {
            Patient patient = new();

            patient.Id = reader.GetInt32(0);
            patient.Email = reader.GetString(1);
            patient.PhoneNumber = reader.GetString(2);
            patient.BirthDate = reader.GetString(3);
            patient.InsuranceNumber = reader.GetInt32(4);
            patient.FirstName = reader.GetString(6);
            patient.LastName = reader.GetString(7);
            patient.Password = reader.GetString(8);
            patient.Role = reader.GetString(9);

            return patient;
        }
    }
}
