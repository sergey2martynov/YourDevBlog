﻿using Application.Dtos.Blog;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.ViewModels;
using AboutMe.Web.Extensions;
using Core.Enums;

namespace AboutMe.Web.Controllers
{
    [Authorize]
    public class NotesController : BaseController
    {
        private readonly IPostService _postService;

        public NotesController(IPostService postService
            )
        {
            _postService = postService;
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
