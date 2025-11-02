using Portable.FunctionalUnits.Odontogramas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class DeleteOdontogramaCommand : BaseCommand<DeleteOdontogramaResponse>
    {
        public int Id { get; set; }

        public DeleteOdontogramaCommand() { }

    }
}