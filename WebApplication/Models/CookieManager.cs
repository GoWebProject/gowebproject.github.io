using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WebApplication.Models
{
    public class CookieManager
    {
        public static async Task WriteCookies(IJSRuntime jsRuntime, string cookieName, string cookieValue)
        {
            await jsRuntime.InvokeAsync<object>("WriteCookie.WriteCookie", cookieName, cookieValue,
                DateTime.Now.AddDays(30));
        }

        public static async Task<string> ReadCookies(IJSRuntime jsRuntime, string cookieName)
        {
            return await jsRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", cookieName);
        }
    }
}