using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreatePostDto
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
        public Guid UserId { get; set; }
    }
}
