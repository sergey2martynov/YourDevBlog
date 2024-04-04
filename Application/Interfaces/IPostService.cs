using Application.Dtos.Blog;
using Application.ViewModels;
using Core.Entities;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<FeedPostVM>> GetAllPublicPosts();
        Task<PostDetailsVM> GetPost(Guid id, Guid userId);
        Task<List<BlogPostVM>> GetPublicPostsByUser(Guid userId);
        Task<List<NoteVM>> GetPrivatePostsByUser(Guid userId);
        Task Create(CreatePostDTO createPostDto, Guid userId);
        Task CreateComment(CreateCommentDTO createCommentDto, Guid userId);
        Task<Post> UpdatePost(UpdatePostDTO dto);
    }
}
