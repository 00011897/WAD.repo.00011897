using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models;

namespace University.Controllers
{
    public class CourseworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courseworks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Courseworks.Include(c => c.Course).Include(c => c.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courseworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursework = await _context.Courseworks
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursework == null)
            {
                return NotFound();
            }

            return View(coursework);
        }

        // GET: Courseworks/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: Courseworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Deadline,CourseId,StudentId")] Coursework coursework)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", coursework.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", coursework.StudentId);
            return View(coursework);
        }

        // GET: Courseworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursework = await _context.Courseworks.FindAsync(id);
            if (coursework == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", coursework.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", coursework.StudentId);
            return View(coursework);
        }

        // POST: Courseworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Deadline,CourseId,StudentId")] Coursework coursework)
        {
            if (id != coursework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseworkExists(coursework.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", coursework.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", coursework.StudentId);
            return View(coursework);
        }

        // GET: Courseworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursework = await _context.Courseworks
                .Include(c => c.Course)
                .Include(c => c.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursework == null)
            {
                return NotFound();
            }

            return View(coursework);
        }

        // POST: Courseworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coursework = await _context.Courseworks.FindAsync(id);
            _context.Courseworks.Remove(coursework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseworkExists(int id)
        {
            return _context.Courseworks.Any(e => e.Id == id);
        }
    }
}
