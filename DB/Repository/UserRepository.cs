using System.Data;
using System.Data.SQLite;
using ZdravotniSystem.DB.Entity;

namespace ZdravotniSystem.DB.Repository
{

    public interface IUserRepository
    {
        void Save(User user, string role);
        User GetOneByEmail(string email);
    }

    public class UserRepository : AbstractRepository, IUserRepository
    {
        public void Save(User user, string role)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "INSERT OR REPLACE INTO doctor (" +
                        "first_name, last_name, email, password, role" +
                        ") VALUES('{0}', '{1}', '{2}', '{3}', '{4}' );",
            user.FirstName, user.LastName, user.Email,
            BCrypt.Net.BCrypt.HashPassword(user.Password), role);

            cmd.ExecuteNonQuery();
        }

        public User GetOneByEmail(string email)
        {

            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT p FROM users WHERE email = '{0}' ;", email);
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return reader.GetInt32(0);

            return default;
        }
    }
}
