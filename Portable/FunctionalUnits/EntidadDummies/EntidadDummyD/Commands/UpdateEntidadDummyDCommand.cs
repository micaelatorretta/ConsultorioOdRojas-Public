using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Commands
{
    public class UpdateEntidadDummyDCommand : BaseCommand<UpdateEntidadDummyDResponse>
    {
        public UpdateEntidadDummyDCommand() { }

        public EntidadDummyDDTO EntidadDummyD { get; set; } = null!;
    }
}
