namespace WebApplication.Models
{
    public class User
    {
        public const int AdminLevel = 1;
        public const int UserLevel = 0;
        
        public User(string username, string email, string pwd, string fullName, int level = UserLevel, string passwordSalt = null, string rfgRating = "1500", string miscRating = "")
        {
            Username = username;
            PasswordHash = pwd;
            Email = email;
            FullName = fullName;
            RfgRating = rfgRating;
            MiscRating = miscRating;
            PasswordSalt = passwordSalt;
            AccessLevel = level;
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordSalt { get; set; }
        public string RfgRating { get; set; }
        public string MiscRating { get; set; }
        public int AccessLevel { get; set; }
    }
}