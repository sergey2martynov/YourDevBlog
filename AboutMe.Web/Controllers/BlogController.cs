using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AboutMe.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _postService;

        public BlogController(IPostService postService) 
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var isPrivate = false;
            var posts = await _postService.GetAll(isPrivate);
            var blog = new BlogDto
            {
                Posts = posts
            };

            return View(blog);
        }

        public async Task<IActionResult> PostDetails(Guid id)
        {
            var post = await _postService.GetPost(id);            
            return View(post);
        }

        public IActionResult CreatePost() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
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
