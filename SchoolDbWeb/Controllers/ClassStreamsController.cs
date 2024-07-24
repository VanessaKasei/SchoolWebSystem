using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolDbWeb.Data;
using SchoolDbWeb.Models; 

namespace SchoolDbWeb.Controllers
{
    public class ClassStreamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassStreamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassStreams
        public async Task<IActionResult> Index()
        {
            var classStreams = await _context.ClassStreams.ToListAsync(); // Ensure this is fetching the right model
            return View(classStreams);
        }

        // GET: ClassStreams/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var classStream = await _context.ClassStreams
                .FirstOrDefaultAsync(cs => cs.ClassStreamId == id);

            if (classStream == null)
            {
                return NotFound();
            }

            return View(classStream);
        }
    }
}
