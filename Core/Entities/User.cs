using Microsoft.AspNetCore.Identity;
namespace Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User() => Id = Guid.NewGuid();
        public ICollection<Post> Posts { get; set; }
    }
}
