using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECDI.ViewModel;
using ECDI.Models;
using Microsoft.AspNetCore.Authorization;

namespace ECDI.Controllers {
    [Authorize]
    public class CoursesController : Controller {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Courses1
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Courses.Include(c => c.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses1/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses1/Create
        public IActionResult Create() {
            ViewData["TeacherId"] = new SelectList(
                _context.Teachers.Select(i => new { Id = i.Id, aa = $"{i.FirstName} {i.LastName}" }),
                "Id", "aa"
            );
            return View();
        }

        // POST: Courses1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel courseModel) {
            MemoryStream ms = new MemoryStream();
            byte[] a = null;
            if (courseModel.Photo != null) {
                courseModel.Photo.CopyTo(ms);
                a = ms.ToArray();

            }
            Course course = new Course {
                CreatedDate = DateTime.Now, EndDate = courseModel.EndDate, StartDate = courseModel.StartDate,
                Description = courseModel.Description, Name = courseModel.Name, Photo = a, TeacherId = courseModel.TeacherId,
                Time = courseModel.Time, Type = courseModel.Type, Length = courseModel.Length, Price = courseModel.Price,
                YouTubeURL = courseModel.YouTubeURL, Level = courseModel.Level
            };
            if (ModelState.IsValid) {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(
                _context.Teachers.Select(i => new { Id = i.Id, aa = $"{i.FirstName} {i.LastName}" }),
                "Id", "aa"
            );
            return View(course);
        }

        // GET: Courses1/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null) {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(
                _context.Teachers.Select(i => new { Id = i.Id, aa = $"{i.FirstName} {i.LastName}" }),
                "Id", "aa"
            );
            return View(course);
        }

        // POST: Courses1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseViewModel courseModel) {
            MemoryStream ms = new MemoryStream();
            byte[] a = null;
            if (courseModel.Photo != null) {
                courseModel.Photo.CopyTo(ms);
                a = ms.ToArray();

            }
            Course course = _context.Courses.First(c => c.Id == id);
            course.StartDate = courseModel.StartDate;
            course.EndDate = courseModel.EndDate;
            course.Description = courseModel.Description;
            course.Photo = a;
            course.Name = courseModel.Name;
            course.TeacherId = courseModel.TeacherId;
            course.Price = courseModel.Price;
            course.YouTubeURL = courseModel.YouTubeURL;
            course.Length = courseModel.Length;
            course.Type = courseModel.Type;
            course.Time = courseModel.Time;
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["TeacherId"] = new SelectList(
                _context.Teachers.Select(i => new { Id = i.Id, aa = $"{i.FirstName} {i.LastName}" }),
                "Id", "aa"
            );
            return View(course);
        }

        // GET: Courses1/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Courses == null) {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null) {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Courses == null) {
                return Problem("Entity set 'ApplicationDbContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null) {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id) {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
