using System.Data.SQLite;
using System.Data;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Mapper;
using ZdravotniSystem.DB.Repository;

namespace ZdravotniSystem.Repository
{

    public interface IPatientRepository : IRepository<Patient> { 
        Patient GetOneByEmail(string email);
    }
    public class PatientRepository : AbstractRepository, IPatientRepository
    {
        public Patient GetOne(int id)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT p FROM patient WHERE id = {0} ;", id);
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return PatientMapper.Map(reader);

            return new Patient();
        }

        public Patient GetOneByEmail(string email)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT * FROM user INNER JOIN patient USING(email) WHERE email = {0} ;", email);
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return PatientMapper.Map(reader);

            return new Patient();
        }

        public List<Patient> FindAll()
        {
            List<Patient> patients = new();

            SQLiteCommand cmd = Connection.CreateCommand();

            cmd.CommandText = "SELECT DISTINCT * FROM patient INNER JOIN users USING(email);";
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                patients.Add(PatientMapper.Map(r));
            }

            return patients;

        }

        public void Save(Patient patient)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "INSERT INTO patient (" +
                        "email, phone_number, birthday, insurance_number" +
                        ") VALUES('{0}', '{1}', '{2}', '{3}');",
                        patient.Email, patient.PhoneNumber,
                        patient.BirthDate, patient.InsuranceNumber
                    );

            cmd.ExecuteNonQuery();

        }

        public void Delete(int id)
        {
            SQLiteCommand cmd = new(Connection);
            cmd.CommandText = string.Format("DELETE FROM patient WHERE id = {0};", id);

            cmd.ExecuteNonQuery();
        }

        public void Update(Patient entity)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "UPDATE patient " +
                        "SET first_name = '{1}', last_name = '{2}', email = '{3}', " +
                        "phone_number = '{4}', birthday = '{5}', insurance_number = {6} " +
                        "WHERE id = {0}; ",
                        entity.Id, entity.FirstName, entity.LastName, entity.Email,
                        entity.PhoneNumber, entity.BirthDate, entity.InsuranceNumber
                    );

            cmd.ExecuteNonQuery();
        }
    }
}
