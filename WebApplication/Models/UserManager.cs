using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WebApplication.Models
{
    public class UserManager
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

        public static List<User> GetUsers(string orderby = null)
        {
            var res = new List<User>();
            var dbase = new DBManager();
            var order = orderby == null ? "" : $" order by {orderby}";
            var reader =
                dbase.GetReader(
                    $"select * from accounts{order}");
            User user = null;
            while (reader.Read())
            {
                user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
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
            var reader =
                dbase.GetReader(
                    $"select * from accounts where username='{username}'"); //TODO: SQL INJECTION
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
            var reader = dbase.GetReader($"select username from accounts where username='{username}'");
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
            dbase.InsertCommand(
                $"insert into accounts value('{user.Username}','{user.Email}','{key}','{user.FullName}','{value}',{user.RfgRating},'{user.MiscRating}','{user.AccessLevel}')");
            dbase.Close();
            return true;
        }

        public static bool ChangeUser(string oldUsername, User user)
        {
            var dbase = new DBManager();
            dbase.InsertCommand(
                $"update accounts set username='{user.Username}', email='{user.Email}',full_name='{user.FullName}',rating={user.RfgRating},misc_ratings='{user.MiscRating}',access_level='{user.AccessLevel}' where username='{oldUsername}'");
            dbase.Close();
            return true;
        }
        
        public static bool DeleteUser(string username)
        {
            var dbase = new DBManager();
            dbase.InsertCommand(
                $"delete from accounts where username='{username}'");
            dbase.Close();
            return true;
        }

        public static bool ChangeUserPassword(string username, string pwd)
        {
            var dbase = new DBManager();
            var reader = dbase.GetReader($"select salt from accounts where username='{username}'");
            reader.Read();
            var salt = reader.GetString(0);
            var hash = GenerateHashFromSalt(pwd, salt);
            dbase.Close();
            dbase = new DBManager();
            dbase.InsertCommand($"update accounts set pwd='{hash}' where username='{username}'");
            dbase.Close();
            return true;
        }
    }
}