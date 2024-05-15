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
            CreateMap<CreatePostDTO, Post>()
                .ForMember(dest => dest.MediaFiles, opt => opt.Ignore());
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Post, FeedPostVM>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<Post, BlogPostVM>();
            CreateMap<Post, NoteVM>();
            CreateMap<Post, UpdatePostVM>();
            CreateMap<CommentDTO, CommentVM>();
            CreateMap<User, UserVM>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));
        }        
    }
}
