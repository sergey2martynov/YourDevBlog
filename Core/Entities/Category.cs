namespace Core.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
