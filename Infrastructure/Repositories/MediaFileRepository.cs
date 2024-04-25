using Core.Entities;
using Core.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories
{
    public class MediaFileRepository : Repository<MediaFile>, IMediaFileRepository
    {
        public MediaFileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
