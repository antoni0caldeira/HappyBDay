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
{
    [Authorize]
    public class ConsultantsController : Controller
    {
        private readonly HappyBDayContext _context;

        public ConsultantsController(HappyBDayContext context)
        {
            _context = context;
        }

        // GET: Consultants
        public async Task<IActionResult> Index(string nomePesquisar, int page = 1)
        {
            Pagination pagination = new Pagination
            {
                
                TotalItems = await _context.Consultants.Where(p=> p.Status.Equals(true) && nomePesquisar == null || p.Name.Contains(nomePesquisar)|| p.ConsultantNumber.Contains(nomePesquisar)).CountAsync(),
                CurrentPage = page

            };

            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);
            List<Consultants> consultants = await happyBDayContext.Where(p => p.Status.Equals(true) && nomePesquisar == null || p.Name.Contains(nomePesquisar) || p.ConsultantNumber.Contains(nomePesquisar))
                .OrderBy(c => c.Name)
                .Skip(pagination.PageSize * (page - 1))
                .Take(pagination.PageSize)
                .ToListAsync();

            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Pagination = pagination,
                Consultants = consultants,
                NomePesquisar=nomePesquisar
            };

            foreach (var item in model.Consultants)
            {
                int age = DateTime.Today.Year - item.DateOfBirth.Year;
                item.Age = age;

            }

            return base.View(model);
        }

        //SABER QUEM FAZ ANOS NUM DIA ESPECÍFICO

        public async Task<IActionResult> SelectDay(int day, int month)
        {



            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);


            List<Consultants> consultants = await happyBDayContext.Where(p => p.Status.Equals(true) && (p.DateOfBirth.Day == day) && (p.DateOfBirth.Month == month))
                .ToListAsync();


            SelectDayViewModel model = new SelectDayViewModel
            {
                Consultants = consultants,
                Day = day,
                Month = month
            };

            if (consultants.Count() == 0)
            {
                ViewBag.Mensagem = "Efectue uma pesquisa";
                return base.View(model);
            }

            return base.View(model);

        }

        public async Task<IActionResult> SelectDay1(int day, int month)
        {



            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);


            List<Consultants> consultants = await happyBDayContext.Where(p => p.Status.Equals(true) && (p.DateOfBirth.Day == day) && (p.DateOfBirth.Month == month))
                .ToListAsync();


            SelectDayViewModel model = new SelectDayViewModel
            {
                Consultants = consultants,
                Day = day,
                Month = month
            };


            if(consultants.Count() == 0)
            {
                ViewBag.Mensagem = "Lista vazia";
                return base.View(model);
            }

            return base.View(model);

        }


        public async Task<IActionResult> IndexOff(string nomePesquisar, int page = 1)
        {

            Pagination pagination = new Pagination
            {

                TotalItems = await _context.Consultants.Where(p => p.Status.Equals(false) && nomePesquisar == null || p.Name.Contains(nomePesquisar)).CountAsync(),
                CurrentPage = page

            };

            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);
            List<Consultants> consultants = await happyBDayContext.Where(p => p.Status.Equals(false) && nomePesquisar == null || p.Name.Contains(nomePesquisar))
                .OrderBy(c => c.Name)
                .Skip(pagination.PageSize * (page - 1))
                .Take(pagination.PageSize)
                .ToListAsync();

            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Pagination = pagination,
                Consultants = consultants,
                NomePesquisar = nomePesquisar
            };

            return base.View(model);
        }

        //SABER QUEM FAZ ANOS NESTE MÊS
        public async Task<IActionResult> IndexMonth(string nomePesquisar, int page = 1)
        {

            Pagination pagination = new Pagination
            {

                TotalItems = await _context.Consultants.Where(p => p.Status.Equals(true) && nomePesquisar == null || p.Name.Contains(nomePesquisar)).CountAsync(),
                CurrentPage = page

            };

            DateTime today = DateTime.Today;
            var dia1 = new DateTime(today.Year, today.Month, 1);
            DateTime finaldia = dia1.AddMonths(1).AddMinutes(-1);
            
            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);
            List<Consultants> consultants = await happyBDayContext.Where(c => (c.DateOfBirth.Month >= dia1.Month  && c.DateOfBirth.Month <= finaldia.Month))

                .Where(c => c.Status.Equals(true) && nomePesquisar == null || c.Name.Contains(nomePesquisar))
                .OrderBy(c => c.Name)
                .Skip(pagination.PageSize * (page - 1))
                .Take(pagination.PageSize)
                .ToListAsync();

            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Pagination = pagination,
                Consultants = consultants,
                NomePesquisar = nomePesquisar
            };

            foreach (var item in model.Consultants)
            {
                int age = DateTime.Today.Year - item.DateOfBirth.Year;
                item.Age = age;

            }

            return base.View(model);
        }

        //SABER QUEM FAZ ANOS NESTE DIA
        public async Task<IActionResult> IndexDay(string nomePesquisar, int page = 1)
        {

            Pagination pagination = new Pagination
            {

                TotalItems = await _context.Consultants.Where(p => p.Status.Equals(true) && nomePesquisar == null || p.Name.Contains(nomePesquisar)).CountAsync(),
                CurrentPage = page

            };

            DateTime today = DateTime.Today;

            var happyBDayContext = _context.Consultants.Include(c => c.IdDepartmentsNavigation);
            List<Consultants> consultants = await happyBDayContext.Where(c => (c.DateOfBirth.Month == today.Month) && (c.DateOfBirth.Day == today.Day))

                .Where(c => c.Status.Equals(true) && nomePesquisar == null || c.Name.Contains(nomePesquisar))
                .OrderBy(c => c.Name)
                .Skip(pagination.PageSize * (page - 1))
                .Take(pagination.PageSize)
                .ToListAsync();

            ConsultantsListViewModel model = new ConsultantsListViewModel
            {
                Pagination = pagination,
                Consultants = consultants,
                NomePesquisar = nomePesquisar
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
