using System.Security.Claims;

namespace AboutMe.Web.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetId(this ClaimsPrincipal principal)
            => principal.HasClaim(q => q.Type == ClaimTypes.NameIdentifier)
                ? Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier).Value)
                : Guid.Empty;
    }
}
