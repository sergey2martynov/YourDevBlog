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
            var posts = await _postService.GetAll();
            var blog = new BlogDto
            {
                Posts = posts
            };

            return View(blog);
        }

        public IActionResult CreatePost() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            await _postService.Create(createPostDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PostDetails(Guid id)
        {
            var post = await _postService.GetPost(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {            
            createCommentDto.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _postService.CreateComment(createCommentDto);
            return RedirectToAction("PostDetails", "Blog", new { id = createCommentDto.PostId });
        }
    }
}
