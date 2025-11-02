using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.ObrasSociales.Commands
{
    public class DeleteObraSocialCommand : BaseCommand<DeleteObraSocialResponse>
    {
        public int Id { get; set; }

        public DeleteObraSocialCommand() { }

    }
}