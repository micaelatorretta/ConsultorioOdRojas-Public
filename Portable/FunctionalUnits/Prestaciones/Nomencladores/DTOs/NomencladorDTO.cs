using Portable.Enums;
using Portable.FunctionalUnits.ObrasSociales.DTOs;
using Shared.Portable.Base;

namespace Portable.FunctionalUnits.Prestaciones.DTOs
{
    public class NomencladorDTO : BaseDTO
    {
        public NomencladorDTO() { }
        public string CodigoNacional { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Importe { get; set; }
        /// <summary>
        /// Es un color sugerido en formato HEX (#RRGGBB), para pintar la pieza o la cara en el odontograma cuando aplicás esa prestación.
        /// Ejemplos:
        // Obturación → azul #2196f3
        // Exodoncia → rojo #d32f2f
        // Sellador → verde #43a047
        /// </summary>
        public string? ColorHexSugerido { get; set; }
        /// <summary>
        /// Indica si la prestación se aplica a una cara específica del diente o a la pieza completa.
        /// true → la prestación se asocia a una cara dental(ej.obturación en cara Oclusal).
        /// false → se aplica a toda la pieza(ej.exodoncia, corona completa, pieza ausente).
        /// </summary>
        public bool RequiereCara { get; set; } = true;
        ///// <summary>
        ///// Define si se pueden registrar varias veces la misma prestación sobre la misma cara o pieza.
        ///// true → se puede aplicar más de una(ej.múltiples obturaciones en diferentes fechas).
        ///// false → solo puede existir una(ej.exodoncia, corona). Si ya está aplicada, no deberías permitir otra igual.
        ///// </summary>
        //public bool PermiteMultiple { get; set; } = true;
        public ObraSocialDTO? ObraSocial { get; set; }

    }
}
