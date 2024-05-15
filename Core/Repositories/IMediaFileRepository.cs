using Core.Entities;

namespace Core.Repositories
{
    public interface IMediaFileRepository : IRepository<MediaFile>
    {
        Task AddRangeAsync(IEnumerable<MediaFile> mediaFile);
    }
}
