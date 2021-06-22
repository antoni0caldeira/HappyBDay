using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Web;
using HappyBDay.Models;
using HappyBDay.Data;
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace HappyBDay.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly HappyBDayContext _context;

        public DeletePersonalDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<DeletePersonalDataModel> logger, 
            HappyBDayContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, Users users)
        {
            var userToDelete = _context.Users.AsNoTracking().SingleOrDefault(c => c.Id == id);

            //Update User Status

            //var userStatus = _context.Users.FindAsync(userToDelete);
            users.Email = userToDelete.Email;
            users.IdProfile = userToDelete.IdProfile;
            users.Status = false;
            users.Username = userToDelete.Username;

            _context.Update(users);
            await _context.SaveChangesAsync();


            //Delete User Login.

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            var deletedUser = await _userManager.FindByNameAsync(userToDelete.Email);
            var result = await _userManager.DeleteAsync(deletedUser);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{deletedUser}'.");
            }

            //wait _signInManager.SignOutAsync();

            //_logger.LogInformation("User with ID '{deletedUser}' deleted themselves.", deletedUser);

            return RedirectToAction("Index", "Users");
        }
    }
}
