using System.Data.SQLite;
using System.Data;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Mapper;

namespace ZdravotniSystem.DB.Repository
{
    public interface IWorkingHoursRepository : IRepository<WorkingHours>
    {
        List<WorkingHours> FindAllByDoctorId(int doctorId);
    }
    public class WorkingHoursRepository : AbstractRepository, IWorkingHoursRepository
    {

        public WorkingHours GetOne(int id)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = $"SELECT * FROM working_hours WHERE id = {id} ;";
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return WorkingHoursMapper.Map(reader);

            return new WorkingHours();
        }

        public List<WorkingHours> FindAll()
        {
            List<WorkingHours> workingHours = new();

            SQLiteCommand cmd = Connection.CreateCommand();

            cmd.CommandText = "SELECT DISTINCT * FROM working_hours";
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                workingHours.Add(WorkingHoursMapper.Map(r));
            }

            return workingHours;

        }

        public List<WorkingHours> FindAllByDoctorId(int doctorId)
        {
            List<WorkingHours> workingHours = new();

            SQLiteCommand cmd = Connection.CreateCommand();

            cmd.CommandText = String.Format("SELECT DISTINCT * FROM working_hours WHERE doctor_id = {0}", doctorId);
            cmd.CommandType = CommandType.Text;
            SQLiteDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                workingHours.Add(WorkingHoursMapper.Map(r));
            }

            return workingHours;

        }

        public void Save(WorkingHours workingHours)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "INSERT OR REPLACE INTO working_hours (" +
                        "day_of_week, hour_from, hours_count, doctor_id" +
                        ") VALUES('{0}', {1}, {2}, {3} );",
                        workingHours.DayOfWeek, workingHours.HourFrom,
                        workingHours.HoursCount, workingHours.DoctorId
                    );

            cmd.ExecuteNonQuery();

        }

        public void Delete(int id)
        {
            SQLiteCommand cmd = new(Connection)
            {
                CommandText = string.Format("DELETE FROM working_hours WHERE id = {0};", id)
            };

            cmd.ExecuteNonQuery();
        }

        public void Update(WorkingHours entity)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "UPDATE working_hours " +
                        "SET doctor_id = {1}, hour_from = {2}, hours_count = {3}, day_of_week = '{4}' " +
                        "WHERE id = {0}; ", entity.Id, entity.DoctorId, entity.HourFrom, entity.HoursCount, entity.DayOfWeek
                    );

            cmd.ExecuteNonQuery();
        }
    }
}
