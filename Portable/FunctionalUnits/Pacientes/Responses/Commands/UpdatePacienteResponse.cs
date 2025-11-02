using Portable.FunctionalUnits.Pacientes.DTOs;
using Shared.Portable.Base.Responses;

namespace Portable.FunctionalUnits.Pacientes.Responses
{
    public class UpdatePacienteResponse : BaseResponse
    {
        public UpdatePacienteResponse()
        {

        }

        public PacienteDTO? Paciente { get; set; } 
    }
}