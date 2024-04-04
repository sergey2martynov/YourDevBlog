using Application.ViewModels;

namespace Application.Dtos.Blog
{
    public class CommentDTO
    {
        public UserVM User { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
