using Application.Interfaces;
using Core.Entities;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
