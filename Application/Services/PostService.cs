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

        public async Task<List<FeedPostVm>> GetAllPublicPosts()
        {
            var posts = await _postRepository.GetAll()
                .Include(p => p.User)
                .Where(p => !p.IsPrivate)
                .Select(p => _mapper.Map<Post, FeedPostVm>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<List<PrivatePostVm>> GetPrivatePostsByUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var posts = await _postRepository.GetAll()
                .Where(p => p.IsPrivate && p.UserId == user.Id)
                .Select(p => _mapper.Map<Post, PrivatePostVm>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<List<BlogPostVm>> GetPublicPostsByUser(Guid userId)
        {
            var posts = await _postRepository.GetAll()
                .Where(p => p.UserId == userId && !p.IsPrivate)
                .Select(p => _mapper.Map<BlogPostVm>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<PostDetailsVm> GetPost(Guid id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            var post = await _postRepository.GetById(id)                
                .Include(q => q.Comments.OrderBy(c => c.CreatedOn))
                .Include(p => p.User)
                .Select(p => _mapper.Map<PostDetailsDto>(p))
                .SingleOrDefaultAsync();

            if (post == null)
                throw new NotFoundException(nameof(Post), id);

            var postVm = _mapper.Map<PostDetailsVm>(post);            

            if(post.UserId == user.Id)
                postVm.IsCanEdit = true;

            return postVm;
        }

        public async Task<GetUpdatePostDto> GetPostForUpdate(Guid id)
        {
            var post = await _postRepository.GetById(id).SingleOrDefaultAsync();
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
            var post = await _postRepository.GetById(dto.Id).SingleOrDefaultAsync();
            post.Message = dto.Message;
            await _postRepository.SaveChangesAsync();

            return post;
        }
    }
}
