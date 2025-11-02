using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class UpdateCaraDentalCommand : BaseCommand<UpdateCaraDentalResponse>
    {
        public UpdateCaraDentalCommand() { }

        public CaraDentalDTO CaraDental { get; set; } = null!;
    }
}
