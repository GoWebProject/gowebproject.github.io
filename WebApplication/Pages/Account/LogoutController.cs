using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Pages.Account
{
    [Route("/Account/Logout")]
    public class LogoutController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Response.Cookies.Delete("username");
            return Redirect("~/Account/Login");
        }
    }
}