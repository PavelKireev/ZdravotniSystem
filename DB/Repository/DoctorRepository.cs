using System.Data.SQLite;
using System.Data;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Mapper;
using ZdravotniSystem.DB.Repository;

namespace ZdravotniSystem.Repository
{
    public interface IDoctorRepository : IRepository<Doctor> {
        Doctor GetOneByEmail(string email);
        void DeleteByEmail(string email);
    }

    public class DoctorRepository : AbstractRepository, IDoctorRepository
    {
        public Doctor GetOne(int id)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT * FROM doctor WHERE id = {0} ;", id);
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return DoctorMapper.Map(reader);

            return new Doctor();
        }

        public List<Doctor> FindAll()
        {
            List<Doctor> doctors = new();

            SQLiteCommand cmd = Connection.CreateCommand();

            cmd.CommandText = "SELECT DISTINCT * FROM doctor INNER JOIN users USING(email); ";
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                doctors.Add(DoctorMapper.Map(r));
            }

            return doctors;

        }

        public void Save(Doctor doctor)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format( 
                        "INSERT OR REPLACE INTO doctor (" +
                        "first_name, last_name, email, office_number" +
                        ") VALUES('{0}', '{1}', '{2}', {3} );",
                        doctor.FirstName, doctor.LastName,
                        doctor.Email, doctor.OfficeNumber
                    );

            cmd.ExecuteNonQuery();

        }

        public void Delete(int id)
        {
            SQLiteCommand cmd = new(Connection)
            {
                CommandText = string.Format("DELETE FROM doctor WHERE id = {0};", id)
            };

            cmd.ExecuteNonQuery();
        }

        public void Update(Doctor entity)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "UPDATE doctor " +
                        "SET office_number = '{1}' " +
                        "WHERE email = '{0}'; ", entity.Email, entity.OfficeNumber
                    );

            cmd.ExecuteNonQuery();
        }

        public Doctor GetOneByEmail(string email)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT * FROM doctor INNER JOIN users USING(email) WHERE email = '{0}' ;", email);
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return DoctorMapper.Map(reader);

            return new Doctor();
        }

        public void DeleteByEmail(string email)
        {
            SQLiteCommand cmd = new(Connection)
            {
                CommandText = string.Format("DELETE FROM doctor WHERE email = '{0}';", email)
            };

            cmd.ExecuteNonQuery();
        }
    }
}
