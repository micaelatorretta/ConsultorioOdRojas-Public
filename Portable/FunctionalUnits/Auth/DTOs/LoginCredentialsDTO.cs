using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Auth.DTOs
{
    public class LoginCredentialsDTO : BaseDTO
    {
        public LoginCredentialsDTO() { }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
