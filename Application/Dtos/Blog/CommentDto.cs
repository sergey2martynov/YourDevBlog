namespace Application.Dtos.Blog
{
    public class CommentDto
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
