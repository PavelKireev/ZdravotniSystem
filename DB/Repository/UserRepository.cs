using System.Data;
using System.Data.SQLite;
using ZdravotniSystem.DB.Entity;
using ZdravotniSystem.DB.Mapper;

namespace ZdravotniSystem.DB.Repository
{
    public interface IUserRepository
    {
        void Save(User user, string role);
        User GetOneByEmail(string email);
        bool DoesEmailExist(string email);
        void Update(User user, string role);
        void DeleteByEmail(string email);
    }

    public class UserRepository : AbstractRepository, IUserRepository
    {
        public void Save(User user, string role)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
                        "INSERT OR REPLACE INTO users (" +
                        "first_name, last_name, email, password, role" +
                        ") VALUES('{0}', '{1}', '{2}', '{3}', '{4}' );",
            user.FirstName, user.LastName, user.Email,
            BCrypt.Net.BCrypt.HashPassword(user.Password), role);

            cmd.ExecuteNonQuery();
        }

        public User GetOneByEmail(string email)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT * FROM users WHERE email = '{0}' ;", email);
            cmd.CommandType = CommandType.Text;

            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return UserMapper.Map(reader);

            return default;
        }

        public bool DoesEmailExist(string email)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText = string.Format("SELECT COUNT(*) FROM users WHERE email = '{0}' ;", email);
            cmd.CommandType = CommandType.Text;
            int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

            return rowCount > 0;
        }

        public void Update(User user, string role)
        {
            SQLiteCommand cmd = new(Connection);

            cmd.CommandText =
                string
                    .Format(
            "UPDATE users " +
            "SET first_name = '{0}', last_name = '{1}', email = '{2}', " +
            "role = '{3}' " +
            "WHERE email = '{2}'; ",
            user.FirstName, user.LastName, user.Email, role);

            cmd.ExecuteNonQuery();
        }

        public void DeleteByEmail(string email)
        {
            SQLiteCommand cmd = new(Connection);
            cmd.CommandText = string.Format("DELETE FROM users WHERE email = '{0}';", email);

            cmd.ExecuteNonQuery();
        }
    }
}
