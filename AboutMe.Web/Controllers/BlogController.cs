using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
