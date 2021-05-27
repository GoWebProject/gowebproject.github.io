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
            var dbase = new DBManager();
            var reader = dbase.GetReader($"select username from accounts where username='{login}' pwd='{pwd}'");
            return reader.Read();
        }
    }
}