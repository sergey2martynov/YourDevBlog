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

        public override async Task<Post> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Post>()
                .Include(q => q.Comments)
                .Where(q => q.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task CreateComment(Comment comment)
        {
            await _dbContext.Set<Comment>().AddAsync(comment);
            _dbContext.SaveChanges();
        }

        public async Task<List<Post>> GetPostsForBlog()
        {
            var posts = _dbContext.Set<Post>().Where(p => !p.IsPrivate);
            return posts.ToList();
        }

        public async Task<List<Post>> GetPostsForNotes(Guid userId)
        {
            var posts = _dbContext.Set<Post>().Where(p => p.IsPrivate && p.UserId == userId);
            return posts.ToList();
        }
    }
}
