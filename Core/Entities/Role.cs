using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<User> Users { get; set; }
    }
}
