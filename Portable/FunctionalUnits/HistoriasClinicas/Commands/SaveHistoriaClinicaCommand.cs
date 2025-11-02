using Portable.FunctionalUnits.HistoriasClinicas.DTOs;
using Portable.FunctionalUnits.HistoriasClinicas.Responses;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.HistoriasClinicas.Commands
{
    public class SaveHistoriaClinicaCommand : BaseCommand<SaveHistoriaClinicaResponse>
    {
        public SaveHistoriaClinicaCommand() { }

        public HistoriaClinicaDTO HistoriaClinica { get; set; } = null!;
    }
}