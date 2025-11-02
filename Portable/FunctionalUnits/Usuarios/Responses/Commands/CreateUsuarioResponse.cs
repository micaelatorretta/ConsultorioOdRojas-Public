using Portable.FunctionalUnits.Usuarios.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Usuarios.Responses
{
    public class CreateUsuarioResponse : BaseResponse
    {
        public CreateUsuarioResponse()
        {

        }

        public UsuarioDTO? Usuario { get; set; } 
    }
}