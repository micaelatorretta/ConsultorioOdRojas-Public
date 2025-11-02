using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Turnos.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de Turno.
    /// </summary>
    public class GetTurnoPagedQuery : BasePagedQuery<TurnoDTO, GetTurnoPagedResponse>
    {
        public GetTurnoPagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}