using Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    public class BaseController : Controller
    {
        [HttpGet]
        protected IActionResult RedirectToError(string errorMessage)
        {
            return RedirectToAction(PageNames.Error, ControllerNames.Home, new { message = errorMessage });
        }
    }
}
