using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UserManagementController(IUserService userService, UserManager<Client> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
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
                TempData["ErrorMessage"] = UserIdOrRoleCannotBeEmpty;
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
                TempData["ErrorMessage"] = UserDoesNotExist;
                return RedirectToAction(nameof(Index));
            }

            if (await userManager.IsInRoleAsync(user, role))
            {
                TempData["ErrorMessage"] = UserAlreadyInRole;
                return RedirectToAction(nameof(Index));
            }

            bool assignResult = await userService.AssignUserToRoleAsync(userId, role);

            if (!assignResult)
            {
                TempData["ErrorMessage"] = FailedToAssignRole;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = RoleAssigned;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role))
            {
                TempData["ErrorMessage"] = UserIdOrRoleCannotBeEmpty;
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
                TempData["ErrorMessage"] = FailedToRemoveRole;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = RoleRemoved;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = UserIdOrRoleCannotBeEmpty;
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
                TempData["ErrorMessage"] = FailedToDeleteUser;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = UserDeleted;
            return RedirectToAction(nameof(Index));
        }
    }
}
