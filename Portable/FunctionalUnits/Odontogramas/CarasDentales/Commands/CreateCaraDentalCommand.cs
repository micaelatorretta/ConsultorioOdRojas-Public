using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class CreateCaraDentalCommand : BaseCommand<CreateCaraDentalResponse>
    {
        public CreateCaraDentalCommand() { }

        public CaraDentalDTO CaraDental { get; set; } = null!;
    }
}