using Portable.FunctionalUnits.Pacientes.DTOs;
using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Pacientes.Commands
{
    public class UpdatePacienteCommand : BaseCommand<UpdatePacienteResponse>
    {
        public UpdatePacienteCommand() { }

        public PacienteDTO Paciente { get; set; } = null!;
    }
}
