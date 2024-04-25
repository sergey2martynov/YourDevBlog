using Application.Dtos.Blog;
using Application.ViewModels;
using AutoMapper;
using Core.Entities;

namespace Core.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CreatePostDTO, Post>();
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Post, FeedPostVM>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<Post, PostDetailsDTO>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => new CommentDTO
                {
                    User = new UserVM
                    {
                        Name = c.User.UserName
                    },
                    Message = c.Message,
                    CreatedOn = c.CreatedOn
                })))
                .ForMember(dest => dest.MediaFileUrls, opt => opt.MapFrom(src => src.MediaFiles.Select(f => f.Url)));
            CreateMap<PostDetailsDTO, PostDetailsVM>();
            CreateMap<Post, BlogPostVM>();
            CreateMap<Post, NoteVM>();
            CreateMap<Post, UpdatePostVM>();
            CreateMap<CommentDTO, CommentVM>();
            CreateMap<User, UserVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }        
    }
}
