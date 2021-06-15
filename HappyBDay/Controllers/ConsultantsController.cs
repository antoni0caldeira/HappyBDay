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

namespace HappyBDay.Controllers
{    [Authorize]
    public class ConsultantsController : Controller
    {
        private readonly HappyBDayContext _context;

        public ConsultantsController(HappyBDayContext context)
        {
            _context = context;
        }

        // GET: Consultants
        public async Task<IActionResult> Index(int page = 1)
        {

            Pagination pagination = new Pagination
            {
                
                TotalItems = await _context.Consultants.CountAsync(),
                CurrentPage = page

            };

            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);
            List<Consultants> consultants = await happyBDayContext
                .OrderBy(c => c.Name)
                .Skip(pagination.PageSize * (page -1))
                .Take(pagination.PageSize)
                .ToListAsync();

            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Pagination = pagination,
                Consultants = consultants
            };
            
            return base.View(model);
        }

        // GET: Consultants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultants = await _context.Consultants
                .Include(c => c.IdDepartmentsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultants == null)
            {
                return NotFound();
            }

            return View(consultants);
        }

        // GET: Consultants/Create
        public IActionResult Create()
        {
            ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Consultants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,Status,DateOfBirth,ConsultantNumber,Gender,IdDepartments")] Consultants consultants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Name", consultants.IdDepartments);
            return View(consultants);
        }

        // GET: Consultants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultants = await _context.Consultants.FindAsync(id);
            if (consultants == null)
            {
                return NotFound();
            }
            ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Name", consultants.IdDepartments);
            return View(consultants);
        }

        // POST: Consultants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,Status,DateOfBirth,ConsultantNumber,Gender,IdDepartments")] Consultants consultants)
        {
            if (id != consultants.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantsExists(consultants.Id))
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
            ViewData["IdDepartments"] = new SelectList(_context.Departments, "Id", "Name", consultants.IdDepartments);
            return View(consultants);
        }

        // GET: Consultants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultants = await _context.Consultants
                .Include(c => c.IdDepartmentsNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultants == null)
            {
                return NotFound();
            }

            return View(consultants);
        }

        // POST: Consultants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultants = await _context.Consultants.FindAsync(id);
            _context.Consultants.Remove(consultants);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultantsExists(int id)
        {
            return _context.Consultants.Any(e => e.Id == id);
        }
    }
}
