using Portable.FunctionalUnits.Usuarios.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Usuarios.Responses
{
    public class GetUsuarioPagedResponse : BasePagedResponse<UsuarioDTO>
    {
        public GetUsuarioPagedResponse() : base() { }
        public GetUsuarioPagedResponse(PaginationData<UsuarioDTO> paginationData)
            : base(paginationData) { }
    }
}
