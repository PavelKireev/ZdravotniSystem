using System.Data;
using System.Data.SQLite;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Mapper;
using ZdravotniSystem.DB.Repository;

namespace ZdravotniSystem.Repository
{

    public interface IAppointmentRepository : IRepository<Appointment> { }

    public class AppointmentRepository : AbstractRepository, IAppointmentRepository
    {
        public Appointment GetOne(int id)
        {
            SQLiteCommand cmd = new();

            cmd.CommandText = string.Format("SELECT * FROM appointment a " +
                "INNER JOIN doctor d ON d.id = a.doctor_id " +
                "INNER JOIN patient p ON p.id = a.patient_id ON WHERE id = {0} ;",
                id
                );

            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return AppointmentMapper.Map(reader);

            return new Appointment();
        }

        public List<Appointment> FindAll()
        {
            List<Appointment> appointments = new();

            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = "SELECT *, " +
                "d.first_name AS doctor_name, " +
                "d.last_name AS doctor_lastname, " +
                "d.email AS doctor_email, " +
                "p.first_name AS patient_name, " +
                "p.last_name AS patient_lastname, " +
                "p.email AS patient_email " +
                "FROM appointment a " +
                "INNER JOIN doctor d ON d.id = a.doctor_id " +
                "INNER JOIN patient p ON p.id = a.pacient_id ";

            cmd.CommandType = CommandType.Text;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                appointments.Add(AppointmentMapper.Map(r));
            }

            return appointments;
        }


        public List<Appointment> FindAllByDoctorId(int id)
        {
            List<Appointment> appointments = new();

            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = String.Format("SELECT *, " +
                "d.first_name AS doctor_name, " +
                "d.last_name AS doctor_lastname, " +
                "d.email AS doctor_email, " +
                "p.first_name AS patient_name, " +
                "p.last_name AS patient_lastname, " +
                "p.email AS patient_email " +
                "FROM appointment a " +
                "INNER JOIN doctor d ON d.id = a.doctor_id " +
                "INNER JOIN patient p ON p.id = a.pacient_id " +
                "WHERE doctor_id = {0} ", id);


            cmd.CommandType = CommandType.Text;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                appointments.Add(AppointmentMapper.Map(r));
            }

            return appointments;
        }


        public void Save(Appointment appointment)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "INSERT OR REPLACE INTO appointment (" +
                        "doctor_id, pacient_id, time" +
                        ") VALUES({0}, {1}, '{2}' );",
                        appointment.Doctor.Id, appointment.Patient.Id, appointment.Time
                    );

            cmd.ExecuteNonQuery();

        }

        public void Delete(int id)
        {
            SQLiteCommand cmd = new(Connection)
            {
                CommandText = string.Format("DELETE FROM appointment WHERE id = {0};", id)
            };

            cmd.ExecuteNonQuery();
        }

        public void test()
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "INSERT OR REPLACE INTO appointment (" +
                        "doctor_id, pacient_id, time" +
                        ") VALUES({0}, {1}, '{2}' );",
                        1, 1, "22 02 02"
                    );

            cmd.ExecuteNonQuery();
        }

        public void Update(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
