using Application.Dtos.Blog;
using Application.Interfaces;
using Core.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

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

            if(createPostDto.IsPrivate)
                return RedirectToAction("Index", "Notes");

            return RedirectToAction("Index", "Blog");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var error = await _postService.CreateComment(createCommentDto);

            if (error == null)
            {
                return RedirectToAction("PostDetails", "Blog", new { id = createCommentDto.PostId });
            }
            else
            {
                TempData["PostDetailsErrorMessage"] = error.ErrorMessage;
                return RedirectToAction("PostDetails", "Blog", new { Id = createCommentDto.PostId });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _postService.DeletePost(id);
            return RedirectToAction("Index", "Notes");
        }
    }
}
