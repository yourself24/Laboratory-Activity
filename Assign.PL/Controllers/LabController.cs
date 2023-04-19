using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assign.BLL.Services;
using Assign.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign.DAL.DataContext;
using Assign.DAL.Models;

namespace Assign.PL.Controllers
{
    public class LabController : Controller
    {
        private readonly SdlabContext _context;
        private readonly ILabService _labService;

        public LabController(SdlabContext context, ILabService labService)
        {
            _context = context;
            _labService = labService;
        }

        // GET: Lab
        public async Task<IActionResult> Index()
        {
            try
            {
                var labs = await _labService.GetAll();
                return View(labs);
            }
            catch
            {
                return Problem("An error occurred while retrieving the list of labs.");
            }
        }


        // GET: Lab/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
            var lab = await _labService.FindLabById(id);

            return View(lab);
        }

        // GET: Lab/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lab/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Lid,LabNo,Date,Title,Description")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _labService.CreateLab(lab);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return Problem("An error occurred while adding the lab.");
                }
            }
            return View(lab);
        }

        // GET: Lab/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Labs == null)
            {
                return NotFound();
            }

            var lab = await _labService.FindLabById(id);
            if (lab == null)
            {
                return NotFound();
            }
            return View(lab);
        }

        // POST: Lab/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Lid,LabNo,Date,Title,Description")] Lab lab)
        {
            if (id != lab.Lid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _labService.UpdateLab(lab.Lid, lab.LabNo, lab.Date, lab.Title, lab.Description);
                }
                catch (ArgumentException ex)
                {
                    return NotFound(ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lab);
        }


        // GET: Lab/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Labs == null)
            {
                return NotFound();
            }

            var lab = await _labService.FindLabById(id);
            if (lab == null)
            {
                return NotFound();
            }

            return View(lab);
        }

        // POST: Lab/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Labs == null)
            {
                return Problem("Entity set 'SdlabContext.Labs'  is null.");
            }

            //var lab = await _labService.FindLabById(id);
            if (id != null)
            {
                _labService.DeleteLab(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool LabExists(int id)
        {
          return (_context.Labs?.Any(e => e.Lid == id)).GetValueOrDefault();
        }
    }
}
