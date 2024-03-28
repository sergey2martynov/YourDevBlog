namespace Application.Dtos.Blog
{
    public class UpdatePostVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
