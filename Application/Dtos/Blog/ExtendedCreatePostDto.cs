namespace Application.Dtos.Blog
{
    public class ExtendedCreatePostDto : CreatePostDto
    {
        public Guid UserId { get; set; }
        public bool IsPrivate { get; set; }
    }
}
