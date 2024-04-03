using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AboutMe.Web.Views.Home
{
    public class ErrorModel : PageModel
    {
        public string Message { get; set; }
        public void OnGet(string message)
        {
            Message = message;
        }
    }
}
