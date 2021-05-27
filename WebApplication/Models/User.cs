namespace WebApplication.Models
{
    public class User
    {
        public User(string username, string email, string pwd, string fullName)
        {
            Username = username;
            PasswordHash = pwd;
            Email = email;
            FullName = fullName;
        }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}