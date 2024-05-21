using AboutMe.Web.ViewComponents.Models;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.ViewComponents
{
    public class PostContentViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string message, List<MediaFileVM> mediaFileUrls)
        {
            var model = new PostContentModel { Title = title, Message = message, MediaFileUrls = mediaFileUrls};
            return View(model);
        }
    }
}
