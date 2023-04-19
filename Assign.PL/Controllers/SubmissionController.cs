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
    public class SubmissionController : Controller
    {
        private readonly SdlabContext _context;
        private readonly ISubmissionService _submissionService;

        public SubmissionController(SdlabContext context, ISubmissionService submissionService)
        {
            _context = context;
            _submissionService = submissionService;
        }

        

        // GET: Submission
        public async Task<IActionResult> Index(int? studId)
        {
            if (studId.HasValue)
            {
                var submissions = await _submissionService.GetAll(studId.Value);
                return View(submissions);
            }
            else
            {
                var sdlabContext = _context.Submissions.Include(s => s.Asign).Include(s => s.Stud);
                return View(await sdlabContext.ToListAsync());
            }
        }


        // GET: Submission/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Submissions == null)
            {
                return NotFound();
            }

            var submission = await _submissionService.GetSubmissionById(id.Value);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // GET: Submission/Create
        public IActionResult Create()
        {
            ViewData["AsignId"] = new SelectList(_context.Assignments, "Asid", "AsName");
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Sid");
            return View();
        }

        // POST: Submission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubId,AsignId,StudId,Grade,Link")] Submission submission)
        {
            if (ModelState.IsValid)
            {
                _submissionService.CreateSubmission(submission);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignId"] = new SelectList(_context.Assignments, "Asid", "Asid", submission.AsignId);
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Sid", submission.StudId);
            return View(submission);
        }

        // GET: Submission/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Submissions == null)
            {
                return NotFound();
            }

            var submission = await _submissionService.GetSubmissionById(id.Value);
            if (submission == null)
            {
                return NotFound();
            }
            ViewData["AsignId"] = new SelectList(_context.Assignments, "Asid", "AsName", submission.AsignId);
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Sid", submission.StudId);
            return View(submission);
        }

        // POST: Submission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubId,AsignId,StudId,Grade,Link")] Submission submission)
        {
            if (id != submission.SubId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                { 
                    await _submissionService.UpdateSubmission(submission.SubId,submission.Grade);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubmissionExists(submission.SubId))
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
            ViewData["AsignId"] = new SelectList(_context.Assignments, "Asid", "AsName", submission.AsignId);
            ViewData["StudId"] = new SelectList(_context.Students, "Sid", "Sid", submission.StudId);
            return View(submission);
        }

        // GET: Submission/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Submissions == null)
            {
                return NotFound();
            }

            var submission = await _submissionService.GetSubmissionById(id.Value);
            if (submission == null)
            {
                return NotFound();
            }

            return View(submission);
        }

        // POST: Submission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Submissions == null)
            {
                return Problem("Entity set 'SdlabContext.Submissions'  is null.");
            }
            var submission = await _submissionService.GetSubmissionById(id);
            if (submission != null)
            {
                _submissionService.DeleteSubmission(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool SubmissionExists(int id)
        {
          return (_context.Submissions?.Any(e => e.SubId == id)).GetValueOrDefault();
        }
    }
}
