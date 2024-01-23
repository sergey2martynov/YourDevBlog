using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
