using Microsoft.AspNetCore.Identity;
namespace Core.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User() => Id = Guid.NewGuid();
    }
}
