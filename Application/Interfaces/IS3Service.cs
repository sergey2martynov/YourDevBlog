using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IS3Service
    {
        Task<string> UploadMediaToS3(IFormFile file);
    }
}
