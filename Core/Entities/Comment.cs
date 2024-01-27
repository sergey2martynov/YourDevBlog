namespace Core.Entities
{
    public class Comment : EntityBase
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
