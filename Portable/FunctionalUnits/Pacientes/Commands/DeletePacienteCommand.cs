using Portable.FunctionalUnits.Pacientes.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Pacientes.Commands
{
    public class DeletePacienteCommand : BaseCommand<DeletePacienteResponse>
    {
        public int Id { get; set; }

        public DeletePacienteCommand() { }

    }
}