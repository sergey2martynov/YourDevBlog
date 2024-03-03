using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AboutMe.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
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

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var post = await _postService.GetPostForUpdate(id);
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDto dto)
        {
            var post = await _postService.UpdatePost(dto);

            return RedirectToAction("Update", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _postService.DeletePost(id);
            return RedirectToAction("Index", "Notes");
        }
    }
}
