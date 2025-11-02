using Portable.FunctionalUnits.Pacientes.DTOs;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Pacientes.Responses
{
    public class GetPacientePagedResponse : BasePagedResponse<PacienteDTO>
    {
        public GetPacientePagedResponse() : base() { }
        public GetPacientePagedResponse(PaginationData<PacienteDTO> paginationData)
            : base(paginationData) { }
    }
}
