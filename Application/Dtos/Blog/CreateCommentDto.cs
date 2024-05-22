using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreateCommentDTO
    {
        [Required]
        public Guid PostId { get; set; }
        [Required]
        [MaxLength(NumberValues.CommentMessageMaxLength)]
        public string Message { get; set; }
    }
}
