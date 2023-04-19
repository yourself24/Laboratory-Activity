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
    public class AttendController : Controller
    {
        private readonly SdlabContext _context;
        private readonly IAttendanceService _attendanceService;


        public AttendController(SdlabContext context,IAttendanceService attendanceService)
        {
            _context = context;
            _attendanceService = attendanceService;
        }

        // GET: Attend
        public async Task<IActionResult> Index(int? labId)
        {
            if (labId.HasValue)
            {
                var attendances = await _attendanceService.GetAll(labId.Value);
                return View(attendances);
            }
            else
            {
                var sdlabContext = _context.Attendances.Include(a => a.Lab).Include(a => a.Stud);
                return View(await sdlabContext.ToListAsync());
            }
        }
       


        // GET: Attend/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _attendanceService.GetAttendanceById(id.Value);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attend/Create
        public IActionResult Create()
        {
            ViewData["LabId"] = new SelectList(_context.Labs, "Lid", "Description");
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Hobby");
            return View();
        }

        // POST: Attend/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttId,LabId,StudId,Present")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _attendanceService.CreateAttendance(attendance);
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabId"] = new SelectList(_context.Labs, "Lid", "Description", attendance.LabId);
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Hobby", attendance.StudId);
            return View(attendance);
        }

        // GET: Attend/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _attendanceService.GetAttendanceById(id.Value);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["LabId"] = new SelectList(_context.Labs, "Lid", "Description", attendance.LabId);
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Hobby", attendance.StudId);
            return View(attendance);
        }

        // POST: Attend/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttId,LabId,StudId,Present")] Attendance attendance)
        {
            if (id != attendance.AttId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _attendanceService.UpdateAttendance(attendance.AttId, attendance.LabId.Value, attendance.StudId.Value,
                        attendance.Present.Value);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttId))
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
            ViewData["LabId"] = new SelectList(_context.Labs, "Lid", "Description", attendance.LabId);
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Hobby", attendance.StudId);
            return View(attendance);
        }

        // GET: Attend/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _attendanceService.GetAttendanceById(id.Value);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'SdlabContext.Attendances'  is null.");
            }

            _attendanceService.DeleteAttendance(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
          return (_context.Attendances?.Any(e => e.AttId == id)).GetValueOrDefault();
        }
    }
}
