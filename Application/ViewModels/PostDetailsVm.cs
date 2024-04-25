namespace Application.ViewModels
{
    public class PostDetailsVM
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<CommentVM> Comments { get; set; }
        public bool IsCanEdit { get; set; }
        public List<string> MediaFileUrls { get; set; }
    }
}
