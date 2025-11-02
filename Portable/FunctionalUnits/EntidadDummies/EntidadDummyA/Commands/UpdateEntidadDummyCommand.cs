using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Commands
{
    public class UpdateEntidadDummyCommand : BaseCommand<UpdateEntidadDummyResponse>
    {
        public UpdateEntidadDummyCommand() { }

        public EntidadDummyDTO EntidadDummy { get; set; } = null!;
    }
}
