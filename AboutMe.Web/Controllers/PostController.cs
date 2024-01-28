using Application.Dtos.Blog;
using Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AboutMe.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            createPostDto.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _postService.Create(createPostDto);

            return RedirectToAction("Index", "Notes");
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
