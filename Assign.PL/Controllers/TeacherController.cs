using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assign.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign.DAL.DataContext;
using Assign.DAL.Models;

namespace Assign.PL.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SdlabContext _context;
        private readonly ITeacherService _teacherService;

        public TeacherController(SdlabContext context,ITeacherService teacherService)
        {
            _context = context;
            _teacherService = teacherService;
        }

        // GET: Teacher
        public async Task<IActionResult> Index()
        {
              return _context.Teachers != null ? 
                          View(await _teacherService.GetAll()) :
                          Problem("Entity set 'SdlabContext.Teachers'  is null.");
        }

        // GET: Teacher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Tid == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tid,Name,Email,Password")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                string passwrd = BCrypt.Net.BCrypt.HashPassword(teacher.Password);
                teacher.Password = passwrd;
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teacher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tid,Name,Email,Password")] Teacher teacher)
        {
            if (id != teacher.Tid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string passwrd = BCrypt.Net.BCrypt.HashPassword(teacher.Password);
                    teacher.Password = passwrd;
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Tid))
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
            return View(teacher);
        }

        // GET: Teacher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Tid == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'SdlabContext.Teachers'  is null.");
            }
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
          return (_context.Teachers?.Any(e => e.Tid == id)).GetValueOrDefault();
        }
    }
}
