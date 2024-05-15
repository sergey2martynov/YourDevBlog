using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static class FormFileExtensions
    {
        public static MediaFileType GetFileType(this IFormFile file)
        {
            var contentType = file.ContentType.ToLower();
            string[] imageExtensions = { "image/jpeg", "image/png", "image/gif" };
            string[] videoExtensions = { "video/mp4", "video/quicktime" };

            if (imageExtensions.Contains(contentType))
            {
                return MediaFileType.Image;
            }
            else if (videoExtensions.Contains(contentType))
            {
                return MediaFileType.Video;
            }
            else
            {
                return MediaFileType.Other;
            }
        }
    }
}
