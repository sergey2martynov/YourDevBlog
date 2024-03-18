using Core.Entities;
using Core.Repositories;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateComment(Comment comment)
        {
            await _dbContext.Set<Comment>().AddAsync(comment);
        }

        public async Task<List<Post>> GetPostsForBlog()
        {
            var posts = await _dbContext.Set<Post>().Where(p => !p.IsPrivate).OrderByDescending(p => p.CreatedOn).ToListAsync();
            
            return posts;
        }

        public async Task<List<Post>> GetPostsForNotes(Guid userId)
        {
            var posts = await _dbContext.Set<Post>().Where(p => p.IsPrivate && p.UserId == userId)
                .OrderByDescending(p => p.CreatedOn).ToListAsync();
            return posts;
        }
    }
}
