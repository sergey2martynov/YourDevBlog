using Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error(string message)
        {
            ViewData[ViewDataFields.ErrorMessage] = message;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
