namespace Application.Dtos.Blog
{
    public class PostDetailsDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public Guid UserId { get; set; }
    }
}
