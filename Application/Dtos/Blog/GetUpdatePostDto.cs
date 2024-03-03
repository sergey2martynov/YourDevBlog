namespace Application.Dtos.Blog
{
    public class GetUpdatePostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
