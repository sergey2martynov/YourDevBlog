namespace Application.Dtos.Blog
{
    public class PostDetailsVm
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public List<CommentDto> Comments { get; set; }
        public bool IsCanEdit {  get; set; }
    }
}
