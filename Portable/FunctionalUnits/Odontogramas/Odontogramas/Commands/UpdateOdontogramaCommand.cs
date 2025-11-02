using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class UpdateOdontogramaCommand : BaseCommand<UpdateOdontogramaResponse>
    {
        public UpdateOdontogramaCommand() { }

        public OdontogramaDTO Odontograma { get; set; } = null!;
    }
}
