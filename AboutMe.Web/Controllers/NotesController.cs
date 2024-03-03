using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createPostDto);
            }

            createPostDto.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _postService.Create(createPostDto);

            return RedirectToAction("Index");
        }
    }
}
