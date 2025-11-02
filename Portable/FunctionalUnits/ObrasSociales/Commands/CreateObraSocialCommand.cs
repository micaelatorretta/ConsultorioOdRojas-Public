using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.ObrasSociales.Commands
{
    public class CreateObraSocialCommand : BaseCommand<CreateObraSocialResponse>
    {
        public CreateObraSocialCommand() { }

        public ObraSocialDTO ObraSocial { get; set; } = null!;
    }
}