using Domain.Enums;

namespace Application.Dtos.User
{
    public class RegisterUserRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public Roles Role { get; set; } 
    }
}
