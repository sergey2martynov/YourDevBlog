using AboutMe.Web.Extensions;
using Application.Dtos.Blog;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AboutMe.Web.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostService postService,
            IPostRepository postRepository,
            IMapper mapper
            )
        {
            _postService = postService;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDTO createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(PageNames.PostDetails.ToString(), ControllerNames.Blog.ToString(),
                    new { id = createCommentDto.PostId });
            }

            var userId = User.GetId();
            await _postService.CreateComment(createCommentDto, userId);

            return RedirectToAction(PageNames.PostDetails.ToString(), ControllerNames.Blog.ToString(),
                new { id = createCommentDto.PostId });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var post = await _postRepository.GetById(id)
                .Select(p => _mapper.Map<UpdatePostVM>(p))
                .SingleOrDefaultAsync();

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDTO dto)
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
        public async Task<IActionResult> Delete([FromServices] IS3Service s3Service, Guid id)
        {
            var post = await _postRepository.GetById(id)
                .Include(p => p.MediaFiles)
                .Select(p => new {Id = p.Id, MediaFileUrls = p.MediaFiles.Select(f => f.Url), IsPrivate = p.IsPrivate})
                .SingleOrDefaultAsync();

            if(post == null)
            {
                return RedirectToError(ErrorMessages.PostNotFound);
            }

            await s3Service.DeleteMediaFromS3(post.MediaFileUrls);
            _postRepository.Delete(new Post {Id = post.Id });
            await _postRepository.SaveChangesAsync();

            if (post.IsPrivate)
                return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Notes.ToString());

            return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Blog.ToString());
        }
    }
}
