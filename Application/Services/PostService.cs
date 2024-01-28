using Application.Dtos.Blog;
using Application.Interfaces;
using Core.Entities;
using Core.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
            var post = await _postRepository.GetByIdAsync(id);
            var result = post.Adapt<PostDetailsDto>();
            return result;
        }

        public async Task Create(CreatePostDto createPostDto)
        {
            var post = createPostDto.Adapt<Post>();
            await _postRepository.AddAsync(post);
        }

        public async Task CreateComment(CreateCommentDto createCommentDto)
        {
            var comment = createCommentDto.Adapt<Comment>();
            await _postRepository.CreateComment(comment);
        }
    }
}
