namespace Application.ViewModels
{
    public class PostDetailsVm
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<CommentVm> Comments { get; set; }
        public bool IsCanEdit { get; set; }
    }
}
