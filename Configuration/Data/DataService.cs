using System.Data.SQLite;

namespace ZdravotniSystem.Configuration.Data
{
    public class DataService
    {

        private readonly IConfiguration _config;

        public DataService(IConfiguration config)
        {
            _config = config;
        }

        public SQLiteConnection InitDbConnection()
        {
            SQLiteConnection connection;
            string dbName = _config["dbName"];
            

            if (!System.IO.File.Exists(dbName))
            {
                SQLiteConnection.CreateFile(dbName);
                connection = new SQLiteConnection(dbName);
                connection.Open();
                RunInitialMigration(connection);
            }
            else
            {
                connection = new SQLiteConnection(dbName);
                connection.Open();
            }

            return connection;
        }

        public static void RunInitialMigration(SQLiteConnection connection)
        {
            string sql =
                "CREATE TABLE IF NOT EXISTS appointment (" +
                "id INTEGER PRIMARY KEY, " +
                "doctor_id INTEGER NOT NULL, " +
                "pacient_id INTEGER NOT NULL," +
                "time TEXT NOT NULL" +
                "); " +
                "CREATE TABLE IF NOT EXISTS doctor (" +
                "id INTEGER PRIMARY KEY, " +
                "first_name TEXT NOT NULL, " +
                "last_name TEXT NOT NULL, " +
                "email TEXT NOT NULL, " +
                "office_number INTEGER NOT NULL " +
                ");" +
                "CREATE TABLE IF NOT EXISTS patient (" +
                "id INTEGER PRIMARY KEY, " +
                "first_name TEXT NOT NULL, " +
                "last_name TEXT NOT NULL, " +
                "email TEXT NOT NULL, " +
                "phone_number TEXT, " +
                "birthday TEXT NOT_NULL, " +
                "insurance_number INTEGER NOT NULL" +
                "); " +
                "CREATE TABLE IF NOT EXISTS working_hours (" +
                "id INTEGER PRIMARY KEY, " +
                "day_of_week TEXT NOT_NULL, " +
                "hour_from INTEGER NOT NULL, " +
                "hours_count INTEGER NOT NULL, " +
                "doctor_id INTEGER NOT NULL" +
                ");";

            SQLiteCommand command = new(sql, connection);
            command.ExecuteNonQuery();
        }

        public static void CloseDBConnection()
        {
            SQLiteConnection connection = DataConfiguration.Instance.DbConnection;

            if (connection != null)
                connection.Close();
        }
    }
}
