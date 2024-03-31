using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class UpdatePostDto
    {
        [Required]
        public Guid Id { get; set; }
        [MaxLength]
        public string Message { get; set; }
    }
}
