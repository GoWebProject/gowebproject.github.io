namespace WebApplication.Services
{
    public class AccountManagerService
    {
        public static bool IsLoggedIn { get; private set; }
        
        public AccountManagerService()
        {
            IsLoggedIn = false; //TODO: логин
        }
    }
}