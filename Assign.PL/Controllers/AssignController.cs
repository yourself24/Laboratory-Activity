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
    public class AssignController : Controller
    {
        private readonly SdlabContext _context;
        private readonly IAssignService _assignmentService;

        public AssignController(SdlabContext context, IAssignService assignService)
        {
            _assignmentService = assignService;
            _context = context;
        }

        // GET: Assign
        public async Task<IActionResult> Index(int? labId)
        {
            if (labId.HasValue)
            {
                var assignments = await _assignmentService.GetAll(labId.Value);
                return View(assignments);
            }
            else
            {
                var sdlabContext = _context.Assignments.Include(a => a.LidNavigation);
                return View(await sdlabContext.ToListAsync());
            }
        }

        // GET: Assign/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Assignments == null)
            {
                return NotFound();
            }

            var assignment = await _assignmentService.GetAssignmentById(id.Value);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assign/Create
        public IActionResult Create()
        {
            ViewData["Lid"] = new SelectList(_context.Labs, "Lid", "Description");
            return View();
        }

        // POST: Assign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Asid,Lid,AsName,Deadline,AsDesc")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _assignmentService.CreateAssignment(assignment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Lid"] = new SelectList(_context.Labs, "Lid", "Description", assignment.Lid);
            return View(assignment);
        }

        // GET: Assign/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Assignments == null)
            {
                return NotFound();
            }

            var assignment = await _assignmentService.GetAssignmentById(id.Value);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["Lid"] = new SelectList(_context.Labs, "Lid", "Description", assignment.Lid);
            return View(assignment);
        }

        // POST: Assign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Asid,Lid,AsName,Deadline,AsDesc")] Assignment assignment)
        {
            if (id != assignment.Asid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _assignmentService.UpdateAssignment(assignment.Asid, assignment.Lid.Value,assignment.AsName , assignment.Deadline,
                        assignment.AsDesc);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Asid))
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
            ViewData["Lid"] = new SelectList(_context.Labs, "Lid", "Description", assignment.Lid);
            return View(assignment);
        }

        // GET: Assign/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Assignments == null)
            {
                return NotFound();
            }

            var assignment = await _assignmentService.GetAssignmentById(id.Value);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Assignments == null)
            {
                return Problem("Entity set 'SdlabContext.Assignments'  is null.");
            }
            var assignment =  await _assignmentService.GetAssignmentById(id);
            if (assignment != null)
            {
                _assignmentService.DeleteAssignment(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
          return (_context.Assignments?.Any(e => e.Asid == id)).GetValueOrDefault();
        }
    }
}
