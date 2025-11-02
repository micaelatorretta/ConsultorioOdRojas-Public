using Portable.FunctionalUnits.Turnos.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Turnos.Responses
{
    public class GetTurnoPagedResponse : BasePagedResponse<TurnoDTO>
    {
        public GetTurnoPagedResponse() : base() { }
        public GetTurnoPagedResponse(PaginationData<TurnoDTO> paginationData)
            : base(paginationData) { }
    }
}
