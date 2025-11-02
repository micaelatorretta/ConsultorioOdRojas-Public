using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Commands
{
    public class DeleteEntidadDummyCommand : BaseCommand<DeleteEntidadDummyResponse>
    {
        public int Id { get; set; }

        public DeleteEntidadDummyCommand() { }

    }
}