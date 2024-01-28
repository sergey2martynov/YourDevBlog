using Core.Entities;

namespace Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task CreateComment(Comment comment);
        Task<List<Post>> GetPostsForBlog();
        Task<List<Post>> GetPostsForNotes(Guid userId);
    }
}
