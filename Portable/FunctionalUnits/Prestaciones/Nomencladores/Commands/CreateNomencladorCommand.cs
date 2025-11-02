using Portable.FunctionalUnits.Prestaciones.DTOs;
using Portable.FunctionalUnits.Prestaciones.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Prestaciones.Commands
{
    public class CreateNomencladorCommand : BaseCommand<CreateNomencladorResponse>
    {
        public CreateNomencladorCommand() { }

        public NomencladorDTO Nomenclador { get; set; } = null!;
    }
}