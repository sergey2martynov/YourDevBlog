namespace Application.Dtos.Blog
{
    public class CreatePostDto
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public bool IsPrivate { get; set; }
        public Guid UserId { get; set; }
    }
}
