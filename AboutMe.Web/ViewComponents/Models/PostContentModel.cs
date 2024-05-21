using Application.ViewModels;

namespace AboutMe.Web.ViewComponents.Models
{
    public class PostContentModel
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public List<MediaFileVM> MediaFileUrls { get; set; }
    }
}
