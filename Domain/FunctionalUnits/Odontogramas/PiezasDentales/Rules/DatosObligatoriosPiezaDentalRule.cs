using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.Enums;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.PiezasDentales.Rules
{
    public class DatosObligatoriosPiezaDentalRule : BaseBusinessRule
    {
        private PiezaDental _piezaDental { get; set; }
        public DatosObligatoriosPiezaDentalRule(PiezaDental piezaDental)
        {
            _piezaDental = piezaDental;
        }
        public override bool IsBroken()
        {
            // Validación de NumeroPieza
            if (_piezaDental.NumeroPieza < 11 || _piezaDental.NumeroPieza > 85)
            {
                AddErrorMessage($"El {nameof(PiezaDental.NumeroPieza)} de la {nameof(PiezaDental)} es obligatorio y debe estar entre 11 y 85.");
            }

            // Validación de Cuadrante
            if (!Enum.IsDefined(typeof(TipoCuadrante), _piezaDental.Cuadrante))
            {
                AddErrorMessage($"El {nameof(PiezaDental.Cuadrante)} de la {nameof(PiezaDental)} es obligatorio y debe ser válido.");
            }

            return HasErrorMessages();
        }
    }
}
