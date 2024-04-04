using Application.Interfaces;
using Application.Services;
using Core.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application.AppStart
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            return services;
        }
    }
}
