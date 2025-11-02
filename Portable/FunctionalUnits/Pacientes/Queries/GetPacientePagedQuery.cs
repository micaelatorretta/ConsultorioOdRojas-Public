using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Portable.Base;
using Shared.Portable.Pagination;

namespace Portable.FunctionalUnits.Pacientes.Queries
{
    /// <summary>
    /// Query paginada, para obtener todos los datos de Paciente.
    /// </summary>
    public class GetPacientePagedQuery : BasePagedQuery<PacienteDTO, GetPacientePagedResponse>
    {
        public GetPacientePagedQuery(PaginationParams paginationParams) : base(paginationParams) { }
      
    }
}