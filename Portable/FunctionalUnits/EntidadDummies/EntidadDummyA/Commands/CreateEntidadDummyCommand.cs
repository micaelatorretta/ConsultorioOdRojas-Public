using Portable.FunctionalUnits.EntidadDummies.DTOs;
using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Commands
{
    public class CreateEntidadDummyCommand : BaseCommand<CreateEntidadDummyResponse>
    {
        public CreateEntidadDummyCommand() { }

        public EntidadDummyDTO EntidadDummy { get; set; } = null!;
    }
}