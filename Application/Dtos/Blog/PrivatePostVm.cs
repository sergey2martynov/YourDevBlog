namespace Application.Dtos.Blog
{
    public class PrivatePostVm
    {
        public Guid Id { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
