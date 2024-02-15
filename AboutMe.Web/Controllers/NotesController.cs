using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    public class NotesController : Controller
    {
        private readonly IPostService _postService;

        public NotesController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAll(true);
            var blog = new BlogDto
            {
                Posts = posts
            };

            return View(blog);
        }

        public async Task<IActionResult> NoteDetails(Guid id)
        {
            var post = await _postService.GetPost(id);
            return View(post);
        }

        public IActionResult CreateNote()
        {
            return View();
        }        
    }
}
