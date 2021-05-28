using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Models;

namespace WebApplication.Pages
{
    public class RegistrationPageModel : ComponentBase
    {
        public void OnGet()
        {
        }


        protected bool OnSubmitListener(string username,string fullname,string email, string _pwd)
        {
            if ((username.Length > 0) && (fullname.Length > 0) && (email.Length > 0) && (_pwd.Length > 0)&&(email.Contains('@')))
            {
                return UserManager.CreateUser(new User(username, email, _pwd, fullname));
            }
            return false;
        }
    }
}
