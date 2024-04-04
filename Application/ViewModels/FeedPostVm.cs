namespace Application.ViewModels
{
    public class FeedPostVM
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public UserVM User { get; set; }
    }
}
