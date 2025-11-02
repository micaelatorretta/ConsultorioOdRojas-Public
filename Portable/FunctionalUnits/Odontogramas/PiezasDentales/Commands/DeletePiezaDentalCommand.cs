using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class DeletePiezaDentalCommand : BaseCommand<DeletePiezaDentalResponse>
    {
        public int Id { get; set; }

        public DeletePiezaDentalCommand() { }

    }
}