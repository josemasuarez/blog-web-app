using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using web_app.Models;

namespace web_app.Controllers
{
    public class UserPrivilegesController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPrivilegeService _privilegeService;

        public UserPrivilegesController(IUserService userService, IPrivilegeService privilegeService)
        {
            _userService = userService;
            _privilegeService = privilegeService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var privileges = await _privilegeService.GetPrivilegesAsync();
            var model = new EditUserPrivilegesViewModel
            {
                User = user,
                AllPrivileges = privileges.ToList(),
                AssignedPrivileges = user.Privileges.Select(p => p.Id).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserPrivilegesViewModel model)
        {
            model.AllPrivileges = (await _privilegeService.GetPrivilegesAsync()).ToList();

            foreach (var privilegeId in model.AllPrivileges.Select(p => p.Id))
            {
                if (model.AssignedPrivileges.Contains(privilegeId))
                {
                    await _userService.AssignPrivilegeAsync(model.User.Id, privilegeId);
                }
                else
                {
                    await _userService.RemovePrivilegeAsync(model.User.Id, privilegeId);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
