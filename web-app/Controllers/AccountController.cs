using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using web_app.Models;

namespace web_app.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;

        public AccountController()
        {
            _authService = new AuthService();
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("JWT") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                
                var jwt = await _authService.AuthenticateUserAsync(model.Username, model.Password);
                if (jwt != null)
                {
                    HttpContext.Session.SetString("JWT", jwt);

                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);
                    var username = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

                    if (!string.IsNullOrEmpty(username))
                    {
                        HttpContext.Session.SetString("Username", username);
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var isRegistered = await _authService.RegisterUserAsync(model);
                if (isRegistered)
                {
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Registration failed. Please try again.");
            }
            return View(model);
        }
    }
}
