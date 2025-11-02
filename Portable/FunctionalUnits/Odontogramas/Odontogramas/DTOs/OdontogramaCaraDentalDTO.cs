using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Portable.FunctionalUnits.Prestaciones.DTOs;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Odontogramas.DTOs
{
    public class OdontogramaCaraDentalDTO : BaseDTO
    {
        public OdontogramaCaraDentalDTO() { }
        public CaraDentalDTO CaraDental { get; set; } = null!;
        public ObraSocialDTO? ObraSocial { get; set; }
        public DateTime FechaPrestacion { get; set; }
        /// <summary>
        /// Pieza dental del odontograma a la cual pertenece la cara dental.
        /// Esta pieza dental del odontograma contiene la pieza dental cargada por default en el sistema.
        /// </summary>
        public OdontogramaPiezaDentalDTO PiezaDental { get; set; } = null!;
        /// <summary>
        /// Representa a la prestación realizada en la cara dental <see cref="CaraDental"/>
        /// </summary>
        public NomencladorDTO? Nomenclador { get; set; }
        public string? ColorHexadecimal { get; set; }

    }
}
