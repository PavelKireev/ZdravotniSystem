using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public class UserMapper
    {
        public static User Map(System.Data.SQLite.SQLiteDataReader reader)
        {
            return new User()
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                Password = reader.GetString(4),
                Role = reader.GetString(5)
            };
        }
    }
}
