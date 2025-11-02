using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.ObrasSociales.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.ObrasSociales.Commands
{
    public class UpdateObraSocialCommand : BaseCommand<UpdateObraSocialResponse>
    {
        public UpdateObraSocialCommand() { }

        public ObraSocialDTO ObraSocial { get; set; } = null!;
    }
}
