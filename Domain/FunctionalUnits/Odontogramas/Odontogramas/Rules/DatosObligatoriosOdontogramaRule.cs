using Domain.FunctionalUnits.Odontogramas.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.Odontogramas.Rules
{
    public class DatosObligatoriosOdontogramaRule : BaseBusinessRule
    {
        private Odontograma _Odontograma { get; set; }
        public DatosObligatoriosOdontogramaRule(Odontograma odontograma)
        {
            _Odontograma = odontograma;
        }
        public override bool IsBroken()
        {



            return HasErrorMessages();
        }
    }
}
