using WebApplication.Models;

namespace WebApplication.Services
{
    public class AccountManagerService
    {
        public User LoggedUser { get; set; }
        
        public AccountManagerService()
        {
            LoggedUser = null;
        }
    }
}