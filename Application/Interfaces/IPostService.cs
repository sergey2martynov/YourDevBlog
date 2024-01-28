﻿using Application.Dtos.Blog;

namespace Application.Interfaces
{
    public interface IPostService
    {
        Task<List<GetPostDto>> GetAll(bool isPrivate);
        Task<PostDetailsDto> GetPost(Guid id);
        Task Create(CreatePostDto createPostDto);
        Task CreateComment(CreateCommentDto createCommentDto);
    }
}
