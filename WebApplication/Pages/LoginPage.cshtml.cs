using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebApplication.Models;


namespace WebApplication.Pages
{
    public class LoginPageModel : ComponentBase
    {
        public void OnGet()
        {
        }

        protected bool OnSubmitListener(string login, string pwd)
        {
            return UserManager.GetUser(login, pwd) != null;
        }
    }
}