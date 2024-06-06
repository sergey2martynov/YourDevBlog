using Core.Entities;
using Core.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
