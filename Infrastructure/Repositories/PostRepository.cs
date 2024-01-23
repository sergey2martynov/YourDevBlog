using Core.Entities;
using Core.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


    }
}
