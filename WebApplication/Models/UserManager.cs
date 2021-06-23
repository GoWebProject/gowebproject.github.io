using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace WebApplication.Models
{
    public static class UserManager
    {
        public static User GetUser(string username, string pwd)
        {
            var user = GetUser(username);
            if (user == null) return null;
            if (GenerateHashFromSalt(pwd, user.PasswordSalt) != user.PasswordHash)
            {
                user = null;
            }

            return user;
        }

        public static List<User> GetUsers(string orderBy = null)
        {
            var res = new List<User>();
            var dbase = new DBManager();
            var order = orderBy == null ? "" : $" order by {orderBy}";
            var reader =
                dbase.GetReader(
                    new MySqlCommand($"select * from accounts{order}"));
            while (reader.Read())
            {
                var user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                    reader.GetInt32(7),
                    reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6));
                res.Add(user);
            }

            dbase.Close();
            return res;
        }

        public static User GetUser(string username)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select * from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            var reader = dbase.GetReader(cmd);
            User user = null;
            if (reader.Read())
            {
                user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                    reader.GetInt32(7),
                    reader.GetString(4), reader.GetInt32(5).ToString(), reader.GetString(6));
            }

            dbase.Close();
            return user;
        }

        public static bool UserExists(string username)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select username from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            var reader = dbase.GetReader(cmd);
            var rvalue = reader.Read();
            dbase.Close();
            return rvalue;
        }

        private static KeyValuePair<string, string> GenerateHash(string s)
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var stringSalt = Convert.ToBase64String(salt);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(s, salt, KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));
            return new KeyValuePair<string, string>(hash, stringSalt);
        }

        private static string GenerateHashFromSalt(string s, string strSalt) => Convert.ToBase64String(
            KeyDerivation.Pbkdf2(s, Convert.FromBase64String(strSalt), KeyDerivationPrf.HMACSHA1, 1000, 256 / 8));

        public static bool CreateUser(User user) //At this point we have password stored in plaintext, not its hash
        {
            var dbase = new DBManager();
            if (UserExists(user.Username)) return false;
            var (key, value) = GenerateHash(user.PasswordHash);
            var cmd = new MySqlCommand(
                $"insert into accounts value(@username,@email,'{key}',@fullname,'{value}',{user.RfgRating},@miscrating,'{user.AccessLevel}')");
            cmd.Parameters.AddStringsWithValues(new[]
            {
                "@username", user.Username, "@fullname", user.FullName, "@email", user.Email, "@miscrating",
                user.MiscRating
            });
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        public static bool ChangeUser(string oldUsername, User user)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand(
                $"update accounts set username=@username, email=@email,full_name=@fullname,rating={user.RfgRating},misc_ratings=@miscrating,access_level='{user.AccessLevel}' where username='{oldUsername}'");
            cmd.Parameters.AddStringsWithValues(new[]
            {
                "@username", user.Username, "@fullname", user.FullName, "@email", user.Email, "@miscrating",
                user.MiscRating
            });
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        public static bool DeleteUser(string username)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("delete from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        public static bool ChangeUserPassword(string username, string pwd)
        {
            var dbase = new DBManager();
            var cmd = new MySqlCommand("select salt from accounts where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            var reader = dbase.GetReader(cmd);
            reader.Read();
            var salt = reader.GetString(0);
            var hash = GenerateHashFromSalt(pwd, salt);
            dbase.Close();
            dbase = new DBManager();
            cmd = new MySqlCommand("update accounts set pwd='{hash}' where username=@username");
            cmd.Parameters.AddWithValue("@username", username);
            dbase.InsertCommand(cmd);
            dbase.Close();
            return true;
        }

        private static void AddStringsWithValues(this MySqlParameterCollection collection,
            IReadOnlyList<string> queries)
        {
            for (var i = 0; i < queries.Count; i += 2)
                collection.AddWithValue(queries[i], queries[i + 1]);
        }
    }
}