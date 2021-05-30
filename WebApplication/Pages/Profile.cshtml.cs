using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;

        public ProfileModel(ILogger<ProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        protected bool OnSubmitListener(string username, string fullname, string email, string _pwd)
        {
            if ((username.Length > 0) && (fullname.Length > 0) && (email.Length > 0) && (_pwd.Length > 0) && (email.Contains('@')))
            {
                return UserManager.ChangeUser(,new User(username, email, _pwd, fullname));
            }
            return false;
        }
    }
}