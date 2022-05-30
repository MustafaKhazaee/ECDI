using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECDI.Models;
using ECDI.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace ECDI.Controllers {
    [Authorize]
    public class TeachersController : Controller {
        private readonly ApplicationDbContext _context;

        public TeachersController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index() {
            return View(await _context.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null) {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Introduction,Email,Mobile1,Mobile2,Photo")] Teacher teacher) {
        public async Task<IActionResult> Create(TeacherViewModel teacher) {
            MemoryStream ms = new MemoryStream();
            byte[] a = null;
            if (teacher.Photo != null) {
                teacher.Photo.CopyTo(ms);
                a = ms.ToArray();

            }
            Teacher t = new Teacher {
                FirstName = teacher.FirstName, LastName = teacher.LastName, Email = teacher.Email, Photo = a,
                Introduction = teacher.Introduction, Mobile1 = teacher.Mobile1, Mobile2 = teacher.Mobile2,
                Facebook = teacher.Facebook
            };
            if (ModelState.IsValid) {
                _context.Add(t);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherViewModel teacher) {
            MemoryStream ms = new MemoryStream();
            byte[] a = null;
            if (teacher.Photo != null) {
                teacher.Photo.CopyTo(ms);
                a = ms.ToArray();

            }
            Teacher c = _context.Teachers.First(t => t.Id == id);
            c.FirstName = teacher.FirstName;
            c.LastName = teacher.LastName;
            c.Email = teacher.Email;
            c.Facebook = teacher.Facebook;
            c.Mobile1 = teacher.Mobile1;
            c.Mobile2 = teacher.Mobile2;
            c.Introduction = teacher.Introduction;
            c.Photo = a;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacher == null) {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id) {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
