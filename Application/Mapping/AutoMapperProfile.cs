using Application.Dtos.Blog;
using AutoMapper;
using Core.Entities;

namespace Core.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<CreatePostDto, Post>();
        }        
    }
}
