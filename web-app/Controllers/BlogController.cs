using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using web_app.Models;

namespace web_app.Controllers
{
    public class BlogController : Controller
    {
        public BlogController()
        {
            
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
