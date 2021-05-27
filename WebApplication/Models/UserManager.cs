namespace WebApplication.Models
{
    public class UserManager
    {
        public static User GetUser(string login, string pwd)
        {
            var dbase = new DBManager();
            var reader = dbase.GetReader($"select username from accounts where username='{login}' and pwd='{pwd}'");
            User user = null;
            if (reader.Read())
            {
                user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
            }
            dbase.Close();
            return user;
        }
        
        public static bool CreateUser(User user)
        {
            //TODO
            return true;
        }
    }
}