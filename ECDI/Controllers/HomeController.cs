using Microsoft.AspNetCore.Mvc;
namespace ECDI.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Courses() {
            return View();
        }
        public IActionResult CourseDetails () {
            return View();
        }
        public IActionResult TeacherProfile () {
            return View();
        }
        public IActionResult Login () {
            ViewBag.dontShow = true;
            return View();
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