using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class DeleteCaraDentalCommand : BaseCommand<DeleteCaraDentalResponse>
    {
        public int Id { get; set; }

        public DeleteCaraDentalCommand() { }

    }
}