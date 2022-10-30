using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public class PatientMapper
    {
        public static Patient Map(System.Data.SQLite.SQLiteDataReader reader)
        {
            Patient patient = new();

            patient.Id = reader.GetInt32(0);
            patient.FirstName = reader.GetString(1);
            patient.LastName = reader.GetString(2);
            patient.Email = reader.GetString(3);
            patient.PhoneNumber = reader.GetString(4);
            patient.BirthDate = reader.GetString(5);
            patient.InsuranceNumber = reader.GetInt32(6);

            return patient;
        }
    }
}
