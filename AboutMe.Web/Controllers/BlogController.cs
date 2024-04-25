using AboutMe.Web.Controllers;
using AboutMe.Web.Extensions;
using Application.Dtos.Blog;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AboutMe.Controllers
{
    [Authorize]
    public class BlogController : BaseController
    {
        private readonly IPostService _postService;        

        public BlogController(IPostService postService
            ) 
        {
            _postService = postService;            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var posts = await _postService.GetPublicPostsByUser(userId);

            var blog = new BlogVM
            {
                Posts = posts
            };

            return View(blog);
        }

        [HttpGet]
        public async Task<IActionResult> PostDetails(Guid id)
        {
            var userId = User.GetId();
            var post = await _postService.GetPost(id, userId);
            return View(post);
        }

        [HttpGet]
        public IActionResult CreatePost() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDTO createPostDto)
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
