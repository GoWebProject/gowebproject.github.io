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

        string Username { get; set; }
        string PasswordHash { get; set; }
        string Email { get; set; }
        string FullName { get; set; }
    }
}