using Domain.FunctionalUnits.ObrasSociales.Entities;
using Domain.FunctionalUnits.Prestaciones.Rules;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Prestaciones.Entities
{
    /// <summary>
    /// Entidad que representa a la prestación
    /// </summary>
    public partial class Nomenclador : BaseAggregateEntity
    {
        public Nomenclador() { }
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
        public ObraSocial ObraSocial { get; set; } = null!;
    }

    public partial class Nomenclador
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la instancia this para validar.
                new DatosObligatoriosNomencladorRule(this),
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}