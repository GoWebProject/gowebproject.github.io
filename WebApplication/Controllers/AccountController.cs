using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            if (User.Identity == null) LoginPage();
            return View("~/Pages/Profile.cshtml");
        }

        public IActionResult RegistrationPage()
        {
            if (User.Identity != null) Profile();
            return View("~/Pages/RegistrationPage.cshtml");
        }

        public IActionResult LoginPage()
        {
            if (User.Identity != null) Profile();
            return View("~/Pages/LoginPage.cshtml");
        }
    }
}