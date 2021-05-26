using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;

        public ProfileModel(ILogger<ProfileModel> logger,
            AccountManagerService accountManagerService)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}