using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreatePostDTO
    {
        [Required]
        [MaxLength]
        public string Message { get; set; }
        [Required]
        [MaxLength(NumberValues.PostTitleLength)]
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
    }
}
