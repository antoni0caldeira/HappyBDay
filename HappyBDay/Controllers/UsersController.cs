using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HappyBDay.Data;
using HappyBDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HappyBDay.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly HappyBDayContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(HappyBDayContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
           

            var happyBDayContext = _context.Users.Include(c => c.IdProfileNavigation);
            List<Users> users = await happyBDayContext.Where(p => p.Status.Equals(true))
                .OrderBy(c => c.Username)
                .ToListAsync();
            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Users = users
            };

           

            return base.View(model);

        }

        public async Task<IActionResult> IndexOff()
        {
            var happyBDayContext = _context.Users.Include(c => c.IdProfileNavigation);
            List<Users> users = await happyBDayContext.Where(p => p.Status.Equals(false))
                .OrderBy(c => c.Username)
                .ToListAsync();
            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Users = users
            };



            return base.View(model);
        }
        

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.IdProfileNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            ViewData["IdProfile"] = new SelectList(_context.Profile, "Id", "Name");
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UsersRegisterViewModel userInfo, Users users)
        {
            ViewData["IdProfile"] = new SelectList(_context.Profile, "Id", "Name", users.IdProfile);

            IdentityUser user = await _userManager.FindByNameAsync(userInfo.Email);
            
            if (user != null)
            {
                ModelState.AddModelError("Email", "There is already a user with that email");
            }

            user = new IdentityUser(userInfo.Email);
            IdentityResult result = await _userManager.CreateAsync(user, userInfo.Password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Faild to register, please try again later.");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            if (!ModelState.IsValid)
            {
                return View(userInfo);
            }

            Users userOriginal = new Users
            {
                Username = userInfo.Username,
                IdProfile = userInfo.IdProfile,
                Email = userInfo.Email,
                Status = userInfo.Status,

            };
            
              _context.Add(userOriginal);
              await _context.SaveChangesAsync();
              return RedirectToAction(nameof(Index));
           
           
           
        }

        public async Task<IActionResult> Reactivate(int id, Users users)
        {
            ViewData["IdProfile"] = new SelectList(_context.Profile, "Id", "Name", users.IdProfile);

            var userReac = await _context.Users.AsNoTracking().FirstOrDefaultAsync(c=> c.Id ==id);

            IdentityUser user = await _userManager.FindByNameAsync(userReac.Email);

            if (user != null)
            {
                ModelState.AddModelError("Email", "There is already a user with that email");
            }


            user = new IdentityUser(userReac.Email);
            IdentityResult result = await _userManager.CreateAsync(user, "Password#123");
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Faild to register, please try again later.");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(userInfo);
            //}
            if (id != users.Id)
            {
                return NotFound();
            }

            users.Email = userReac.Email;
            users.IdProfile = userReac.IdProfile;
            users.Status = true;
            users.Username = userReac.Username;

           
           _context.Update(users);
           await _context.SaveChangesAsync();


            ViewBag.Mensagem = $"Reactivated user {users.Email} and reset password: Password#123 ";
            return View("Sucess");



        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            ViewData["IdProfile"] = new SelectList(_context.Profile, "Id", "Name", users.IdProfile);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,IdProfile,Status")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProfile"] = new SelectList(_context.Profile, "Id", "Name", users.IdProfile);
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .Include(u => u.IdProfileNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
