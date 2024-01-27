using Core.Entities;

namespace Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task CreateComment(Comment comment);
    }
}
