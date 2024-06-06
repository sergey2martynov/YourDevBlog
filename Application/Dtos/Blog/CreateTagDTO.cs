using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Blog
{
    public class CreateTagDTO
    {
        [Required]
        [RegularExpression(@"^[\p{L}\s_]*$")]
        public string Name { get; set; }
    }
}
