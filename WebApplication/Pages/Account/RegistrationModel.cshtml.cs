using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Models;

namespace WebApplication.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (Request.Cookies.ContainsKey("username")) return Redirect("/Account/Profile");
            return Page();
        }
    }
}
