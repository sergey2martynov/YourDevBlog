﻿namespace Application.Dtos.Identity
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
