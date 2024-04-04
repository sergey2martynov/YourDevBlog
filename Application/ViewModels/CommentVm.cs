namespace Application.ViewModels
{
    public class CommentVM
    {
        public UserVM User { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
