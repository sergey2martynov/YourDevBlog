using Application.Dtos.Blog;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Repositories;
using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IS3Service _s3Service;

        public PostService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ICommentRepository commentRepository,
            IS3Service s3Service
            )
        {
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _s3Service = s3Service;
        }

        public async Task<List<FeedPostVM>> GetAllPublicPosts()
        {
            var posts = await _unitOfWork.PostRepository.GetAll()
                .Include(p => p.User)
                .Where(p => !p.IsPrivate)
                .Select(p => _mapper.Map<Post, FeedPostVM>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<List<NoteVM>> GetPrivatePostsByUser(Guid userId)
        {

            var posts = await _unitOfWork.PostRepository.GetAll()
                .Where(p => p.IsPrivate && p.UserId == userId)
                .Select(p => _mapper.Map<Post, NoteVM>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<List<BlogPostVM>> GetPublicPostsByUser(Guid userId)
        {
            var posts = await _unitOfWork.PostRepository.GetAll()
                .Where(p => p.UserId == userId && !p.IsPrivate)
                .Select(p => _mapper.Map<BlogPostVM>(p))
                .ToListAsync();

            return posts;
        }

        public async Task<PostDetailsVM> GetPost(Guid id, Guid userId)
        {
            var post = await _unitOfWork.PostRepository.GetById(id)                
                .Include(q => q.Comments.OrderBy(c => c.CreatedOn)).ThenInclude(q => q.User)
                .Include(p => p.User)
                .Include(p => p.MediaFiles)
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

            var url = await _s3Service.UploadMediaToS3(createPostDto.MediaFile);

            var mediaFile = new MediaFile
            {
                Url = url,
                ContentType = createPostDto.MediaFile.ContentType,
                FileName = createPostDto.MediaFile.FileName,
            };

            post.MediaFiles = new List<MediaFile> { mediaFile };

            await _unitOfWork.MediaFileRepository.AddAsync(mediaFile);
            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();            
        }

        public async Task CreateComment(CreateCommentDTO createCommentDto, Guid userId)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.UserId = userId;
            await _commentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Post> UpdatePost(UpdatePostDTO dto)
        {
            var post = await _unitOfWork.PostRepository.GetById(dto.Id).SingleOrDefaultAsync();
            post.Message = dto.Message;
            post.Preview = TruncateText(post.Message, NumberValues.PostPreviewLength);
            await _unitOfWork.SaveChangesAsync();

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
