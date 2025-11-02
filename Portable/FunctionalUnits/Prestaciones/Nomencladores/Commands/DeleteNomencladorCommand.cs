using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Prestaciones.Commands
{
    public class DeleteNomencladorCommand : BaseCommand<DeleteNomencladorResponse>
    {
        public int Id { get; set; }

        public DeleteNomencladorCommand() { }

    }
}