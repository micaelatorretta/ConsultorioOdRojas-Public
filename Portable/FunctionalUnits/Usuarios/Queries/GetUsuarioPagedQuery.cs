using Portable.FunctionalUnits.Usuarios.DTOs;
using Portable.FunctionalUnits.Usuarios.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Usuarios.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de Usuario.
    /// </summary>
    public class GetUsuarioPagedQuery : BasePagedQuery<UsuarioDTO, GetUsuarioPagedResponse>
    {
        public GetUsuarioPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}