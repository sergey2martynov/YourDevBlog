using Application.Dtos.Blog;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<FeedPostVm>> GetAllPublicPosts();
        Task<PostDetailsVm> GetPost(Guid id);
        Task<GetUpdatePostDto> GetPostForUpdate(Guid id);
        Task<List<BlogPostVm>> GetPublicPostsByUser(Guid userId);
        Task<List<PrivatePostVm>> GetPrivatePostsByUser();
        Task Create(ExtendedCreatePostDto createPostDto);
        Task<ValidationResult> CreateComment(CreateCommentDto createCommentDto);
        Task<Post> UpdatePost(UpdatePostDto dto);
    }
}
