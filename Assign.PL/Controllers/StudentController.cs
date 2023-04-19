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
    public class StudentController : Controller
    {
        private readonly SdlabContext _context;
        private readonly IStudentService _studentService;

        public StudentController(SdlabContext context,IStudentService studentService)
        {
            _studentService = studentService;
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _studentService.GetAll()) :
                          Problem("Entity set 'SdlabContext.Students'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()

        {

            ViewData["Token1"] = new SelectList(_context.Tokens, "Token1", "Token1");
         
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sid,Name,Email,Password,GroupNo,Hobby")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sid,Name,Email,Password,GroupNo,Hobby")] Student student)
        {
            if (id != student.Sid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _studentService.UpdateStudent(student.Sid,student.Name,student.Email,student.Password,student.GroupNo,student.Hobby);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Sid))
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
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentById(id.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SdlabContext.Students'  is null.");
            }
            var student = await _studentService.GetStudentById(id);
            if (student != null)
            {
                _studentService.DeleteStudent(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Sid == id)).GetValueOrDefault();
        }
    }
}
