using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using Core.Repositories;
using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Application.ViewModels;
using AboutMe.Web.Extensions;

namespace AboutMe.Web.Controllers
{
    [Authorize]
    public class NotesController : BaseController
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public NotesController(IPostService postService,
            IMapper mapper,
            IPostRepository postRepository)
        {
            _postService = postService;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetId();
            var posts = await _postService.GetPrivatePostsByUser(userId);
            var blog = new NotesVM
            {
                Posts = posts
            };

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> NoteDetails(Guid id)
        {
            try
            {
                var userId = User.GetId();
                var post = await _postService.GetPost(id, userId);
                return View(post);
            }
            catch (Exception ex)
            {
                return RedirectToError(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreatePostDTO createPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createPostDto);
            }

            var userId = User.GetId();
            await _postService.Create(createPostDto, userId);

            return RedirectToAction(PageNames.Index.ToString());
        }
    }
}
