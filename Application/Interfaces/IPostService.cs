using Application.Dtos.Blog;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<GetPostDto>> GetAll(bool isPrivate);
        Task<PostDetailsDto> GetPost(Guid id);
        Task Create(CreatePostDto createPostDto);
        Task<ValidationResult> CreateComment(CreateCommentDto createCommentDto);
        Task DeletePost(Guid id);
    }
}
