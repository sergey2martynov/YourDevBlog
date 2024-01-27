using Application.Dtos.Blog;
using Application.Interfaces;
using Core.Entities;
using Core.Repositories;
using Mapster;
namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<GetPostDto>> GetAll()
        {
            var posts = await _postRepository.GetAllAsync();
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
