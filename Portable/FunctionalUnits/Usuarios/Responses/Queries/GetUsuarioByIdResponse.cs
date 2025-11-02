using Portable.FunctionalUnits.Usuarios.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Usuarios.Responses
{
    public class GetUsuarioByIdResponse : BaseResponse
    {
        public GetUsuarioByIdResponse() { }

        public UsuarioDTO Usuario { get; set; } = null!;
    }
}