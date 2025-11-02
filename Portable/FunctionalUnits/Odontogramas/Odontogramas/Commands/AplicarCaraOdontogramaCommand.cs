using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class AplicarCaraOdontogramaCommand : BaseCommand<AplicarCaraOdontogramaResponse>
    {
        public AplicarCaraOdontogramaCommand() { }

        public int NomencladorId { get; set; } 
        public string ColorHexadecimal { get; set; } = null!;
        public string? Observacion { get; set; } 
        public byte NumeroPiezaDental { get; set; }
        /// <summary>
        /// Codigo cara dental:
        /// O, M, L, D, V.
        /// </summary>
        public string CaraDental { get; set; } = null!;
        public int OdontogramaId { get; set; } 
    }
}
