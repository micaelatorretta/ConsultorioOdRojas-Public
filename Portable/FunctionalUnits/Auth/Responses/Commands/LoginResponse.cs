using Portable.FunctionalUnits.Usuarios.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Auth.Responses
{
    public class LoginResponse : BaseResponse
    {
        public LoginResponse()
        {

        }

        public UsuarioDTO? Usuario { get; set; } 
    }
}