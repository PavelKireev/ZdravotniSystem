using System.Data.SQLite;
using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public static class AppointmentMapper
    {
        public static Appointment Map(SQLiteDataReader reader)
        {
            Appointment appointment = new();
            Doctor doctor = new();
            Patient patient = new();

            doctor.Id = reader.GetInt32(reader.GetOrdinal("doctor_id"));
            doctor.FirstName = reader.GetString(reader.GetOrdinal("doctor_name"));
            doctor.LastName = reader.GetString(reader.GetOrdinal("doctor_lastname"));
            doctor.Email = reader.GetString(reader.GetOrdinal("doctor_email"));

            patient.Id = reader.GetInt32(reader.GetOrdinal("patient_id"));
            patient.FirstName = reader.GetString(reader.GetOrdinal("patient_name"));
            patient.LastName = reader.GetString(reader.GetOrdinal("patient_lastname"));
            patient.Email = reader.GetString(reader.GetOrdinal("patient_email"));

            appointment.Id = reader.GetInt32(reader.GetOrdinal("id"));
            appointment.Doctor = doctor;
            appointment.Patient = patient;
            appointment.Time = reader.GetString(reader.GetOrdinal("time"));

            return appointment;
        }
    }
}
