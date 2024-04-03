using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    //TODO IdentityCore()
    public class Role : IdentityRole<Guid>
    {
        public ICollection<User> Users { get; set; }
    }
}
