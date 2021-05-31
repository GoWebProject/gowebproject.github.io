using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace WebApplication.Pages.Account
{
    public class PasswordChangeModel : PageModel
    {

        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("username")) return Redirect("/Account/Login");
            return Page();
        }
    }
}