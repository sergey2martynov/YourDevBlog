using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreatePostDTO
    {
        [Required]
        [MaxLength]
        public string Message { get; set; }
        [Required]
        [MaxLength(132)]
        public string Title { get; set; }
    }
}
