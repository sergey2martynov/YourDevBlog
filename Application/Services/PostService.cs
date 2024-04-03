using Application.Dtos.Blog;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository,
            IMapper mapper,
            ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<FeedPostVM>> GetAllPublicPosts()
        {
            var posts = await _postRepository.GetAll()
                .Include(p => p.User)
                .Where(p => !p.IsPrivate)
                .Select(p => _mapper.Map<Post, FeedPostVM>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<List<NoteVM>> GetPrivatePostsByUser(Guid userId)
        {

            var posts = await _postRepository.GetAll()
                .Where(p => p.IsPrivate && p.UserId == userId)
                .Select(p => _mapper.Map<Post, NoteVM>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<List<BlogPostVM>> GetPublicPostsByUser(Guid userId)
        {
            var posts = await _postRepository.GetAll()
                .Where(p => p.UserId == userId && !p.IsPrivate)
                .Select(p => _mapper.Map<BlogPostVM>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<PostDetailsVM> GetPost(Guid id, Guid userId)
        {

            var post = await _postRepository.GetById(id)                
                .Include(q => q.Comments.OrderBy(c => c.CreatedOn)).ThenInclude(q => q.User)
                .Include(p => p.User)
                .Select(p => _mapper.Map<PostDetailsDTO>(p))
                .SingleOrDefaultAsync();

            if (post == null)
            {
                throw new NotFoundException(nameof(Post), id);
            }

            var postVm = _mapper.Map<PostDetailsVM>(post);            

            if(post.UserId == userId)
                postVm.IsCanEdit = true;

            return postVm;
        }

        public async Task Create(CreatePostDTO createPostDto, Guid userId)
        {
            var post = _mapper.Map<Post>(createPostDto);
            post.UserId = userId;
            post.Preview = TruncateText(post.Message, NumberValues.PostPreviewLength);

            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();
        }

        public async Task CreateComment(CreateCommentDTO createCommentDto, Guid userId)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.UserId = userId;
            await _commentRepository.AddAsync(comment);
            await _postRepository.SaveChangesAsync();
        }

        public async Task<Post> UpdatePost(UpdatePostDTO dto)
        {
            var post = await _postRepository.GetById(dto.Id).SingleOrDefaultAsync();
            post.Message = dto.Message;
            post.Preview = TruncateText(post.Message, NumberValues.PostPreviewLength);
            await _postRepository.SaveChangesAsync();

            return post;
        }

        private string TruncateText(string text, int length)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= length)
            {
                return text;
            }

            var truncatedText = text.Substring(0, length);
            truncatedText += "...";

            return truncatedText;
        }
    }
}
