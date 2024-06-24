using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
                var jwt = await _authService.AuthenticateUserAsync(model.Username, model.Password);
                if (jwt != null)
                {
                    // Save the JWT in the session
                    HttpContext.Session.SetString("JWT", jwt);

                    // Decode the JWT to extract the username
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);
                    var username = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                    var privileges = token.Claims.FirstOrDefault(c => c.Type == "privileges")?.Value;

                    if (!string.IsNullOrEmpty(username))
                    {
                        HttpContext.Session.SetString("Username", username);

                        // Create the identity and sign in the user
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, username)
                        };

                        // Add roles from the privileges claim
                        var roles = privileges.Split(','); // assuming privileges are comma-separated
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Trim().ToUpper())); // Convert roles to uppercase
                        }

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
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
