using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Turnos.Commands
{
    public class DeleteTurnoCommand : BaseCommand<DeleteTurnoResponse>
    {
        public int Id { get; set; }

        public DeleteTurnoCommand() { }

    }
}