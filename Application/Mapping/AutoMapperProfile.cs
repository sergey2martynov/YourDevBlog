using Application.Dtos.Blog;
using AutoMapper;
using Core.Entities;

namespace Core.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CreatePostDto, ExtendedCreatePostDto>();
            CreateMap<ExtendedCreatePostDto, Post>();
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Post, PostDetailsDto>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => new CommentDto
                {
                    UserName = c.User.UserName,
                    Message = c.Message,
                    CreatedOn = c.CreatedOn
                })));
            CreateMap<PostDetailsDto, PostDetailsVm>();
        }        
    }
}
