using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class UpdatePiezaDentalCommand : BaseCommand<UpdatePiezaDentalResponse>
    {
        public UpdatePiezaDentalCommand() { }

        public PiezaDentalDTO PiezaDental { get; set; } = null!;
    }
}
