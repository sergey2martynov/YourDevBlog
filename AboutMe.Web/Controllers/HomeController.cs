using Application.ViewModels;
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
            var errorVm = new ErrorVM
            {
                Message = message,
            };

            return View(errorVm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
