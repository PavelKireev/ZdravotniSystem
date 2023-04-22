using System.Data.SQLite;

namespace ZdravotniSystem.Configuration.Data
{
    public sealed class DataConfiguration
    {
        public SQLiteConnection DbConnection { get; private set; }

        private DataConfiguration() { }

        private static DataConfiguration _instance = null;
        public static DataConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataConfiguration();
                    string dbName = "Data source = Objednani.sqlite";

                    if (!System.IO.File.Exists(dbName))
                    {
                        SQLiteConnection.CreateFile(dbName);
                        _instance.DbConnection = new SQLiteConnection(dbName);
                        _instance.DbConnection.Open();
                        DataService.RunInitialMigration(_instance.DbConnection);
                    }
                    else
                    {
                        _instance.DbConnection = new SQLiteConnection(dbName);
                        _instance.DbConnection.Open();
                    }
                }
                return _instance;
            }
        }
    }
}
