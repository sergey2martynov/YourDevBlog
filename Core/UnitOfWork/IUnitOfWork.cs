using Core.Repositories;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        IMediaFileRepository MediaFileRepository { get; }
        Task SaveChangesAsync();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
