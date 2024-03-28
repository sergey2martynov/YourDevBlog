namespace Application.Dtos.Blog
{
    public class PostDetailsDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<CommentVm> Comments { get; set; }
        public Guid UserId { get; set; }
    }
}
