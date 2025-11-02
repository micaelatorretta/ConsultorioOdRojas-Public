using Portable.FunctionalUnits.Auth.DTOs;
using Portable.FunctionalUnits.Auth.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Auth.Commands
{
    public class LoginCommand : BaseCommand<LoginResponse>
    {
        public LoginCommand() { }

        public LoginCredentialsDTO LoginCredentials { get; set; } = null!;
    }
}