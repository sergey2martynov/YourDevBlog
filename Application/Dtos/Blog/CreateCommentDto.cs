using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreateCommentDto
    {
        [Required]
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(2200)]
        public string Message { get; set; }
    }
}
