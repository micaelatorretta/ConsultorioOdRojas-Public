using Portable.FunctionalUnits.Usuarios.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Usuarios.Responses
{
    public class UpdateUsuarioResponse : BaseResponse
    {
        public UpdateUsuarioResponse()
        {

        }

        public UsuarioDTO? Usuario { get; set; } 
    }
}