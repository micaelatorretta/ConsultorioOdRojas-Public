using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Pacientes.Queries
{
    public class GetPacienteByIdQuery : BaseQuery<GetPacienteByIdResponse>
    {
        public GetPacienteByIdQuery() { }
      
        public int Id { get; set; }
    }
}