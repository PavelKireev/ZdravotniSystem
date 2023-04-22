using System.Data.SQLite;

namespace ZdravotniSystem.Configuration.Data
{

    public class DataService
    {
        public static SQLiteConnection InitDbConnection()
        {
            SQLiteConnection connection;
            string dbName = "Data source = Objednani.sqlite";
            

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
                "CREATE TABLE IF NOT EXISTS users (" + 
                "id INTEGER PRIMARY KEY, " + 
                "first_name TEXT NOT NULL, " + 
                "last_name TEXT NOT NULL, " + 
                "email TEXT NOT NULL UNIQUE, " + 
                "password TEXT NOT NULL, " + 
                "role TEXT NOT NULL " + 
                "); " + 
                "CREATE TABLE IF NOT EXISTS appointment (" + 
                "id INTEGER PRIMARY KEY, " + 
                "doctor_id INTEGER NOT NULL, " + 
                "patient_id INTEGER NOT NULL, " + 
                "time TEXT NOT NULL " + 
                "); " + 
                "CREATE TABLE IF NOT EXISTS doctor (" + 
                "id INTEGER PRIMARY KEY, " + 
                "email TEXT NOT NULL UNIQUE, " + 
                "office_number INTEGER NOT NULL " + 
                ");" + 
                "CREATE TABLE IF NOT EXISTS patient (" + 
                "id INTEGER PRIMARY KEY, " + 
                "email TEXT NOT NULL UNIQUE, " + 
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
                ");" + 
                "CREATE TABLE IF NOT EXISTS user_token (" + 
                "id INTEGER PRIMARY KEY, " + 
                "key TEXT NOT NULL, " + 
                "value TEXT NOT NULL, " + 
                "exparation_time TEXT NOT NULL " +
                "); " +
                "INSERT OR REPLACE INTO users (first_name, last_name, email, password, role) " +
                "VALUES ('admin', 'admin', 'admin@admin.com', '$2a$10$1heAywaRY7r/ACJlSSK84eiFy59T7D1SZM7lBUdsKb3f9I2xz7sjy', 'ADMIN');";
            
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
