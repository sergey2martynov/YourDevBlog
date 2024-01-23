namespace Application.Dtos.Blog
{
    public class PostDetailsDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
