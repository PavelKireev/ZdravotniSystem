using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Mapper
{
    public class UserTokenMapper
    {
        public static UserToken Map(System.Data.SQLite.SQLiteDataReader reader)
        {
            UserToken userToken = new();

            userToken.Id = reader.GetInt32(0);
            userToken.Key = reader.GetString(1);
            userToken.Value = reader.GetString(2);
            userToken.ExparationTime = reader.GetString(3);

            return userToken;
        }
    }
}
