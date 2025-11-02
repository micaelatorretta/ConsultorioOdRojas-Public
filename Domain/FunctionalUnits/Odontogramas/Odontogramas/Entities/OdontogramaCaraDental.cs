using Domain.FunctionalUnits.ObrasSociales.Entities;
using Domain.FunctionalUnits.Odontogramas.Rules;
using Domain.FunctionalUnits.Prestaciones.Entities;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Odontogramas.Entities
{
    public partial class OdontogramaCaraDental : BaseEntity
    {
        public OdontogramaCaraDental() { }
        public CaraDental CaraDental { get; set; } = null!;
        public ObraSocial? ObraSocial { get; set; } 
        public DateTime FechaPrestacion { get; set; } 
        /// <summary>
        /// Pieza dental del odontograma a la cual pertenece la cara dental.
        /// Esta pieza dental del odontograma contiene la pieza dental cargada por default en el sistema.
        /// </summary>
        public OdontogramaPiezaDental PiezaDental { get; set; } = null!;
        /// <summary>
        /// Representa a la prestación realizada en la cara dental <see cref="CaraDental"/>
        /// </summary>
        public Nomenclador Nomenclador { get; set; } = null!;
        public string? ColorHexadecimal { get; set; }
    }

}