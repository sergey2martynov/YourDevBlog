﻿using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private readonly IPostService _postService;

        public FeedController(IPostService postService) 
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPublicPosts();
            var feed = new FeedVM
            {
                Posts = posts
            };

            return View(feed);
        }
    }
}
