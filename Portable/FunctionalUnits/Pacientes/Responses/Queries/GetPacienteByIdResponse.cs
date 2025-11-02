using Portable.FunctionalUnits.Pacientes.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Pacientes.Responses
{
    public class GetPacienteByIdResponse : BaseResponse
    {
        public GetPacienteByIdResponse() { }

        public PacienteDTO Paciente { get; set; } = null!;
    }
}