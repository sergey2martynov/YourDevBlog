namespace Core.Entities
{
    public class Tag : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
