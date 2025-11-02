using Portable.FunctionalUnits.Odontogramas.DTOs;
using Portable.FunctionalUnits.Odontogramas.Responses;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.Commands
{
    public class EliminarPrestacionOdontogramaCommand : BaseCommand<EliminarPrestacionOdontogramaResponse>
    {
        public EliminarPrestacionOdontogramaCommand() { }

        public byte NumeroPiezaDental { get; set; }
        /// <summary>
        /// Codigo cara dental:
        /// O, M, L, D, V.
        /// </summary>
        public string CaraDental { get; set; } = null!;
        public int OdontogramaId { get; set; } 
    }
}
