using Application.Dtos.Blog;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<GetPostDto>> GetAll(bool isPrivate);
        Task<PostDetailsDto> GetPost(Guid id);
        Task<GetUpdatePostDto> GetPostForUpdate(Guid id);
        Task Create(CreatePostDto createPostDto);
        Task<ValidationResult> CreateComment(CreateCommentDto createCommentDto);
        Task<Post> UpdatePost(UpdatePostDto dto);
        Task DeletePost(Guid id);
    }
}
