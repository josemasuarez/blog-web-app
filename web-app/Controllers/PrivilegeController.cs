using Microsoft.AspNetCore.Mvc;
using web_app.Models;

namespace web_app.Controllers
{
    public class PrivilegeController : Controller
    {
        private readonly IPrivilegeService _privilegeService;

        public PrivilegeController(IPrivilegeService privilegeService)
        {
            _privilegeService = privilegeService;
        }

        public async Task<IActionResult> Index()
        {
            var privileges = await _privilegeService.GetPrivilegesAsync();
            return View(privileges);
        }
    }
}
