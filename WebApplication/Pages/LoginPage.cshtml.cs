using System;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MySql.Data.MySqlClient;
using Ubiety.Dns.Core;


namespace WebApplication.Pages
{
    public class LoginPageModel : ComponentBase
    {
        public void OnGet()
        {
        }

        public void OnSubmitListener(NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            var connectionString = "Server=127.0.0.1; Port=54855; Database=localdb; Uid=azure; Pwd=6#vWHD_$";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            jsRuntime.InvokeVoidAsync("alert", conn.State.ToString() );
            jsRuntime.InvokeVoidAsync("alert", Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"));
            conn.Close();
        }
    }
}