namespace Application.ViewModels
{
    public class FeedPostVm
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
    }
}
