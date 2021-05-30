namespace WebApplication.Models
{
    public class User
    {
        public User(string username, string email, string pwd, string fullName, string passwordSalt = null, int rfg_rating = 1500, string misc_rating = "")
        {
            Username = username;
            PasswordHash = pwd;
            Email = email;
            FullName = fullName;
            RFG_rating = rfg_rating;
            Misc_rating = misc_rating;
            this.PasswordSalt = passwordSalt;
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordSalt { get; set; }
        public int RFG_rating { get; set; }
        public string Misc_rating { get; set; }
    }
}