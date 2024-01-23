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

        public async override Task<IReadOnlyList<Post>> GetAllAsync()
        {
            var posts = await _dbContext.Set<Post>().OrderByDescending(p => p.CreatedOn).ToListAsync();
            return posts;
        }

        public override async Task<Post> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Post>()
                .Include(q => q.C)
                .Where(q => q.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}
