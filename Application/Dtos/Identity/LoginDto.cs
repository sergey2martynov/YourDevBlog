using Core.Entities;
namespace Application.Dtos.Identity
{
    public class LoginDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
