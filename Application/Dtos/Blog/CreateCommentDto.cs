using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreateCommentDto
    {
        [Required]
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
