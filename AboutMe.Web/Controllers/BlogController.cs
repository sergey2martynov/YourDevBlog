using Application.Dtos.Blog;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AboutMe.Controllers
{
    public class BlogController : Controller
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

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createPostDto);
            }

            var post = _mapper.Map<Post>(createPostDto);
            post.UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            post.IsPrivate = false;
            post.Preview = post.Message.Length > 300 ? post.Message.Substring(0, 300) + ".." : post.Message;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
