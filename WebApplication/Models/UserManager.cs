using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WebApplication.Models
{
    public class UserManager
    {
        public static User GetUser(string username, string pwd)
        {
            var dbase = new DBManager();
            var reader = dbase.GetReader($"select username from accounts where username='{username}'"); //TODO: SQL INJECTION
            User user = null;
            if (reader.Read())
            {
                user = new User(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                if (GenerateHashFromSalt(pwd, reader.GetString(4)) != user.PasswordHash)
                {
                    user = null;
                }
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
            dbase.InsertCommand($"insert into accounts value('{user.Username}','{user.Email}','{key}','{user.FullName}','{value}')");
            dbase.Close();
            return true;
        }
    }
}