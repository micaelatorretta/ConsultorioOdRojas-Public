using Portable.FunctionalUnits.EntidadDummies.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.EntidadDummies.Commands
{
    public class DeleteEntidadDummyDCommand : BaseCommand<DeleteEntidadDummyDResponse>
    {
        public int Id { get; set; }

        public DeleteEntidadDummyDCommand() { }

    }
}