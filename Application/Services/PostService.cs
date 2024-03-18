using Application.Dtos.Blog;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Constants;
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
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _postRepository = postRepository;
            _mapper = mapper;
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

        public async Task<PostDetailsVm> GetPost(Guid id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var post = await _postRepository.GetByIdAsync(id)
                .Include(q => q.Comments.OrderBy(c => c.CreatedOn))
                .Include(p => p.User)
                .Select(p => _mapper.Map<PostDetailsDto>(p))
                .SingleOrDefaultAsync();

            var postVm = _mapper.Map<PostDetailsVm>(post);

            if (post == null)
                throw new NotFoundException(nameof(Post), id);

            if(post.UserId == user.Id)
                postVm.IsCanEdit = true;

            return postVm;
        }

        public async Task<GetUpdatePostDto> GetPostForUpdate(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id).SingleOrDefaultAsync();
            var result = post.Adapt<GetUpdatePostDto>();

            return result;
        }

        public async Task Create(ExtendedCreatePostDto createPostDto)
        {
            var post = _mapper.Map<Post>(createPostDto);
            post.Preview = post.Message.Length > 300 ? post.Message.Substring(0, 300) + ".." : post.Message;

            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();
        }

        public async Task<ValidationResult> CreateComment(CreateCommentDto createCommentDto)
        {
            if (string.IsNullOrEmpty(createCommentDto.Message))
            {
                return new ValidationResult(
                ErrorMessages.CommentEmpty,
                new[] { nameof(PostDetailsDto.Comments) });
            }

            var comment = _mapper.Map<Comment>(createCommentDto);
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
    }
}
