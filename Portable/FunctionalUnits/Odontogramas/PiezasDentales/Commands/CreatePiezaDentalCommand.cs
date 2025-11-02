using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class CreatePiezaDentalCommand : BaseCommand<CreatePiezaDentalResponse>
    {
        public CreatePiezaDentalCommand() { }

        public PiezaDentalDTO PiezaDental { get; set; } = null!;
    }
}