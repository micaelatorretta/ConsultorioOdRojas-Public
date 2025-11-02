using Domain.FunctionalUnits.EntidadDummies.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.EntidadDummies.Rules
{
    public class DatosObligatoriosEntidadDummyDRule : BaseBusinessRule
    {
        private EntidadDummyD _entidadDummyD { get; set; }
        public DatosObligatoriosEntidadDummyDRule(EntidadDummyD entidadDummyD)
        {
            _entidadDummyD = entidadDummyD;
        }
        public override bool IsBroken()
        {
            if (string.IsNullOrEmpty(_entidadDummyD.Name))
            {
                AddErrorMessage($"El Nombre de {nameof(EntidadDummyD)} es obligatorio.");
            }

            return HasErrorMessages();
        }
    }
}
