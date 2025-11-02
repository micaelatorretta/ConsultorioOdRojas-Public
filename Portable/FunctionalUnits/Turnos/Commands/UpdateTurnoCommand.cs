using Portable.FunctionalUnits.Turnos.DTOs;
using Portable.FunctionalUnits.Turnos.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Turnos.Commands
{
    public class UpdateTurnoCommand : BaseCommand<UpdateTurnoResponse>
    {
        public UpdateTurnoCommand() { }

        public TurnoDTO Turno { get; set; } = null!;
    }
}
