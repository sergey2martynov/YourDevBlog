using Core.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreatePostDTO
    {
        [Required]
        [MaxLength]
        public string Message { get; set; }
        [Required]
        [MaxLength(NumberValues.PostTitleMaxLength)]
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
        [MaxLength(4)]
        public List<IFormFile> MediaFiles { get; set; } = new List<IFormFile>();
    }
}
