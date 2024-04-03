using AboutMe.Web.Controllers;
using AboutMe.Web.Extensions;
using Application.Dtos.Blog;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.Constants;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AboutMe.Controllers
{
    [Authorize]
    public class BlogController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public BlogController(IPostService postService, IPostRepository postRepository, IMapper mapper) 
        {
            _postService = postService;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _postService.GetPublicPostsByUser(userId);

            var blog = new BlogVm
            {
                Posts = posts
            };

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> PostDetails(Guid id)
        {
            try
            {
                var post = await _postService.GetPost(id);
                return View(post);
            }
            catch (Exception ex)
            {
               return RedirectToError(ex.Message);
            }
        }

        [HttpGet]
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

            var post = _mapper.Map<ExtendedCreatePostDto>(createPostDto);
            post.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            post.IsPrivate = false;
            await _postService.Create(post);

            return RedirectToAction(PageNames.Index.ToString());
        }
    }
}
