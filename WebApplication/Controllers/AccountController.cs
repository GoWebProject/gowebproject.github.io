using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            if (!Request.Cookies.ContainsKey("username")) LoginPage();
            return View("~/Pages/Profile.cshtml");
        }

        public IActionResult RegistrationPage()
        {
            if (Request.Cookies.ContainsKey("username")) Profile();
            return View("~/Pages/RegistrationPage.cshtml");
        }

        public IActionResult LoginPage()
        {
            if (Request.Cookies.ContainsKey("username")) Profile();
            return View("~/Pages/LoginPage.cshtml");
        }
    }
}