using System.Data.Common;
using System.Data.SQLite;
using ZdravotniSystem.Configuration.Data;

namespace ZdravotniSystem.DB.Repository
{
    public abstract class AbstractRepository
    {
        protected static readonly SQLiteConnection Connection = DataConfiguration.Instance.DbConnection;
    }

}
