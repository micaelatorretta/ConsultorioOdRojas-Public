using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Commands
{
    public class CreateEntidadDummyDCommand : BaseCommand<CreateEntidadDummyDResponse>
    {
        public CreateEntidadDummyDCommand() { }

        public EntidadDummyDDTO EntidadDummyD { get; set; } = null!;
    }
}