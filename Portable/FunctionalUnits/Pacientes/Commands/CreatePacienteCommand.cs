using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Pacientes.Commands
{
    public class CreatePacienteCommand : BaseCommand<CreatePacienteResponse>
    {
        public CreatePacienteCommand() { }

        public PacienteDTO Paciente { get; set; } = null!;
    }
}