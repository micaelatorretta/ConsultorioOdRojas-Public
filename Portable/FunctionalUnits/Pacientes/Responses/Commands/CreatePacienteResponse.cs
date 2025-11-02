using Portable.FunctionalUnits.Pacientes.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Pacientes.Responses
{
    public class CreatePacienteResponse : BaseResponse
    {
        public CreatePacienteResponse()
        {

        }

        public PacienteDTO? Paciente { get; set; } 
    }
}