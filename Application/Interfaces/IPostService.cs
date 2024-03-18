using Application.Dtos.Blog;
using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<GetPostDto>> GetAll(bool isPrivate);
        Task<PostDetailsVm> GetPost(Guid id);
        Task<GetUpdatePostDto> GetPostForUpdate(Guid id);
        Task Create(ExtendedCreatePostDto createPostDto);
        Task<ValidationResult> CreateComment(CreateCommentDto createCommentDto);
        Task<Post> UpdatePost(UpdatePostDto dto);
    }
}
