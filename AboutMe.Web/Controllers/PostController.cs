using Application.Dtos.Blog;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.Constants;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AboutMe.Web.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IPostRepository postRepository, IMapper mapper)
        {
            _postService = postService;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var error = await _postService.CreateComment(createCommentDto);

            if (error == null)
            {
                return RedirectToAction(PageNames.PostDetails, ControllerNames.Blog, new { id = createCommentDto.PostId });
            }
            else
            if (!ModelState.IsValid)
            {
                return RedirectToAction(PageNames.PostDetails.ToString(), ControllerNames.Blog.ToString(),
                    new { id = createCommentDto.PostId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var post = await _postRepository.GetById(id)
                .Select(p => _mapper.Map<UpdatePostVm>(p))
                .SingleOrDefaultAsync();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDto dto)
        {
            var post = await _postService.UpdatePost(dto);

            if (post == null)
            {
                return RedirectToError(ErrorMessages.PostNotFound);
            }

            if(post.IsPrivate)
                return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Notes);

            return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Blog);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var post = await _postRepository.GetById(id).SingleOrDefaultAsync();

            if(post == null)
            {
                return RedirectToError(ErrorMessages.PostNotFound);
            }

            await _postRepository.DeleteAsync(post);
            await _postRepository.SaveChangesAsync();

            if (post.IsPrivate)
                return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Notes);

            return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Blog);
        }
    }
}
