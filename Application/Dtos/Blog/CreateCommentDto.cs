namespace Application.Dtos.Blog
{
    public class CreateCommentDto
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}
