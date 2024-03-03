using Application.Dtos.Blog;
using Application.Interfaces;
using Core.Entities;
using Core.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IPostRepository _postRepository;        

        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _postRepository = postRepository;
        }

        public async Task<List<GetPostDto>> GetAll(bool isPrivate)
        {
            var posts = new List<Post>();
            if (!isPrivate)
                posts = await _postRepository.GetPostsForBlog();
            else
            {
                var user = _httpContextAccessor.HttpContext.User;
                Guid userId;
                if (user.Identity.IsAuthenticated)
                {
                    var currentUser = await _userManager.GetUserAsync(user);
                    if (currentUser != null)
                    {
                        userId = currentUser.Id;
                        posts = await _postRepository.GetPostsForNotes(userId);
                    }
                }                
            }
                
            var result = posts.Adapt<List<GetPostDto>>();
            return result;
        }

        public async Task<PostDetailsDto> GetPost(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id).SingleOrDefaultAsync();
            var result = post.Adapt<PostDetailsDto>();

            return result;
        }

        public async Task<GetUpdatePostDto> GetPostForUpdate(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id).SingleOrDefaultAsync();
            var result = post.Adapt<GetUpdatePostDto>();

            return result;
        }

        public async Task Create(CreatePostDto createPostDto)
        {
            var post = createPostDto.Adapt<Post>();
            post.Preview = post.Message.Length > 300 ? post.Message.Substring(0, 300) + ".." : post.Message;

            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();
        }

        public async Task<ValidationResult> CreateComment(CreateCommentDto createCommentDto)
        {
            if (string.IsNullOrEmpty(createCommentDto.Message))
            {
                return new ValidationResult(
                "Comment cannot be empty",
                new[] { nameof(PostDetailsDto.Comments) });
            }

            var user = await _userManager.FindByIdAsync(createCommentDto.UserId.ToString());
            var comment = createCommentDto.Adapt<Comment>();
            comment.UserName = user.UserName;

            await _postRepository.CreateComment(comment);
            await _postRepository.SaveChangesAsync();

            return null;
        }

        public async Task<Post> UpdatePost(UpdatePostDto dto)
        {
            var post = await _postRepository.GetByIdAsync(dto.Id).SingleOrDefaultAsync();
            post.Message = dto.Message;
            await _postRepository.SaveChangesAsync();

            return post;
        }

        public async Task DeletePost(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id).SingleOrDefaultAsync();
            await _postRepository.DeleteAsync(post);
            await _postRepository.SaveChangesAsync();
        }
    }
}
