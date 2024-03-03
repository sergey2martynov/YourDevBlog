using Application.Dtos.Blog;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using Core.Repositories;

namespace AboutMe.Web.Controllers
{
    public class NotesController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public NotesController(IPostService postService, IMapper mapper, IPostRepository postRepository)
        {
            _postService = postService;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAll(true);
            var blog = new BlogDto
            {
                Posts = posts
            };

            return View(blog);
        }

        public async Task<IActionResult> NoteDetails(Guid id)
        {
            var post = await _postService.GetPost(id);
            return View(post);
        }

        public IActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createPostDto);
            }

            var post = _mapper.Map<Post>(createPostDto);
            post.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            post.IsPrivate = true;
            post.Preview = post.Message.Length > 300 ? post.Message.Substring(0, 300) + ".." : post.Message;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
