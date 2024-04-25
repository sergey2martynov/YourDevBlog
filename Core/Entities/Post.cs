namespace Core.Entities
{
    public class Post : EntityBase
    {
        public string Message { get; set; }
        public string Preview { get; set; }
        public string Title { get; set; }
        public ICollection<Comment> Comments { get; set;}
        public bool IsPrivate { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<MediaFile> MediaFiles { get; set; }
    }
}
