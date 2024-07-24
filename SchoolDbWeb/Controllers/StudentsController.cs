using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDbWeb.Data;
using SchoolDbWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDbWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students
                .Include(s => s.ClassStream)
                .ToListAsync();
            return View(students);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students
                .Include(s => s.ClassStream)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Populate the ViewData with a SelectList of ClassStreams
            ViewData["ClassStreamId"] = new SelectList(await _context.ClassStreams.ToListAsync(), "ClassStreamId", "ClassName", student.ClassStreamId);

            return View(student);
        }


        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,ClassStreamId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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

            ViewData["ClassStreamId"] = new SelectList(await _context.ClassStreams.ToListAsync(), "ClassStreamId", "ClassName", student.ClassStreamId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students
                .Include(s => s.ClassStream)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }

        // GET: Students/ByClassStream/5
        public async Task<IActionResult> ByClassStream(int classStreamId)
        {
            var students = await _context.Students
                .Include(s => s.ClassStream)
                .Where(s => s.ClassStreamId == classStreamId)
                .ToListAsync();

            if (students == null || !students.Any())
            {
                return NotFound();
            }

            var classStream = await _context.ClassStreams.FindAsync(classStreamId);
            if (classStream == null)
            {
                return NotFound();
            }

            ViewData["ClassStreamName"] = classStream.ClassName;
            return View(students);
        }

    }
}
