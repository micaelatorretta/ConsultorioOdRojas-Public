using Domain.FunctionalUnits.Odontogramas.Entities;
using Portable.Enums;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.CarasDentales.Rules
{
    public class DatosObligatoriosCaraDentalRule : BaseBusinessRule
    {
        private CaraDental _caraDental { get; set; }
        public DatosObligatoriosCaraDentalRule(CaraDental caraDental)
        {
            _caraDental = caraDental;
        }
        public override bool IsBroken()
        {

            // Validación de Cara dental
            if (!Enum.IsDefined(typeof(TipoCara), _caraDental.CaraDentaria))
            {
                AddErrorMessage($"El {nameof(CaraDental.CaraDentaria)} de la {nameof(CaraDental)} es obligatorio y debe ser válido.");
            }

            return HasErrorMessages();
        }
    }
}
