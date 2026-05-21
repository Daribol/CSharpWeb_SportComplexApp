using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SportComplexApp.Common;
using SportComplexApp.Data.Models;
using SportComplexApp.Services.Data.Contracts;
using SportComplexApp.Web.Controllers;
using static SportComplexApp.Common.ErrorMessages.Users;
using static SportComplexApp.Common.SuccessfulValidationMessages.Users;

namespace SportComplexApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<Client> userManager;
        private readonly IStringLocalizer<SharedResource> sharedLocalizer;

        public UserManagementController(IUserService userService, UserManager<Client> userManager, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.sharedLocalizer = sharedLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var allUsers = await userService.GetAllUsersAsync();
            return View(allUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = sharedLocalizer[UserIdOrRoleCannotBeEmpty].Value;
                return RedirectToAction(nameof(Index));
            }

            bool userExists = await userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = sharedLocalizer[UserDoesNotExist].Value;
                return RedirectToAction(nameof(Index));
            }

            if (await userManager.IsInRoleAsync(user, role))
            {
                TempData["ErrorMessage"] = sharedLocalizer[UserAlreadyInRole].Value;
                return RedirectToAction(nameof(Index));
            }

            bool assignResult = await userService.AssignUserToRoleAsync(userId, role);

            if (!assignResult)
            {
                TempData["ErrorMessage"] = sharedLocalizer[FailedToAssignRole].Value;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = sharedLocalizer[RoleAssigned].Value;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = sharedLocalizer[UserIdOrRoleCannotBeEmpty].Value;
                return RedirectToAction(nameof(Index));
            }

            bool userExists = await userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            bool removeRoleResult = await userService.RemoveUserRoleAsync(userId, role);

            if (!removeRoleResult)
            {
                TempData["ErrorMessage"] = sharedLocalizer[FailedToRemoveRole].Value;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = sharedLocalizer[RoleRemoved].Value;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = sharedLocalizer[UserIdOrRoleCannotBeEmpty].Value;
                return RedirectToAction(nameof(Index));
            }

            bool userExists = await userService.UserExistsByIdAsync(userId);

            if (!userExists)
            {
                return NotFound();
            }

            bool deleteResult = await userService.DeleteUserAsync(userId);

            if (!deleteResult)
            {
                TempData["ErrorMessage"] = sharedLocalizer[FailedToDeleteUser].Value;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = sharedLocalizer[UserDeleted].Value;
            return RedirectToAction(nameof(Index));
        }
    }
}
