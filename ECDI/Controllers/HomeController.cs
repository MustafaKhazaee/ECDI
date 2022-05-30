using ECDI.Models;
using ECDI.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECDI.Controllers {
    public class HomeController : Controller {
        protected readonly ApplicationDbContext context;
        public HomeController (ApplicationDbContext applicationDbContext) => this.context = applicationDbContext;
        public IActionResult Index() {
            List<Course> courses = context.Courses.Include(c => c.Teacher)
                .OrderByDescending(c => c.CreatedDate).Take(100).ToList();
            return View(courses);
        }
        public IActionResult Courses (string course) {
            List<Course> courses = context.Courses.Where(c => c.Type.Equals(course)).Include(c => c.Teacher)
                .OrderByDescending(c => c.CreatedDate).Take(100).ToList();
            return View(courses);
        }
        public IActionResult CourseDetails (int course) {
            return View(context.Courses.Where(c => c.Id == course).Include(c => c.Teacher).First());
        }
        public IActionResult TeacherProfile () {
            return View();
        }
        public IActionResult Login () {
            ViewBag.dontShow = true;
            return View();
        }
        [HttpPost]
        public IActionResult Login (LoginModel loginModel) {
            if (loginModel.Username != null && loginModel.Password != null) {
                string username = loginModel.Username;
                string password = loginModel.Password;
                if (username.Equals("_ECDI_Admin_") && password.Equals("exp_@ECDI*59")) {
                    List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "ECDI Admin"),
                    new Claim(ClaimTypes.Role, "Root")
                };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties authenticationProperties = new AuthenticationProperties {
                        IsPersistent = true,
                        IssuedUtc = DateTime.UtcNow,
                        RedirectUri = "/Courses/Index",
                    };
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authenticationProperties);
                    return RedirectToAction("Index", "Courses");
                }
            }
            return View();
        }

        public IActionResult Logout () {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AboutUs () {
            return View();
        }
        public IActionResult ContactUs () {
            return View();
        }
        public IActionResult Error404 () {
            ViewBag.dontShow = true;
            return View();
        }
        public IActionResult Gallery () {
            return View();
        }
    }
}