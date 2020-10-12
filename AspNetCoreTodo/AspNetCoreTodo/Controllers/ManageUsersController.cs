using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager
                .GetUsersInRoleAsync(Constants.AdministratorRole))
                .ToArray();

            var everyone = await _userManager.Users.ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var selectedUser = await _userManager.Users
                .Where(x => x.Id == id.ToString())
                .SingleOrDefaultAsync();

            if (currentUser == selectedUser) 
            { 
                return BadRequest("Cannot delete current active user."); 
            }

            await _userManager.DeleteAsync(selectedUser);

            return RedirectToAction("Index");
        }
    }
}