﻿using Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    public class BaseController : Controller
    {
        [HttpGet]
        protected IActionResult RedirectToError(string errorMessage)
        {
            return RedirectToAction(PageNames.Error.ToString(), ControllerNames.Home.ToString(), new { message = errorMessage });
        }
    }
}
