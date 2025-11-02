using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class CreateOdontogramaCommand : BaseCommand<CreateOdontogramaResponse>
    {
        public CreateOdontogramaCommand() { }

        public OdontogramaDTO Odontograma { get; set; } = null!;
    }
}