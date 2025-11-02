using Domain.FunctionalUnits.EntidadDummies.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.EntidadDummies.Rules
{
    public class DatosObligatoriosEntidadDummyRule : BaseBusinessRule
    {
        private EntidadDummy _entidadDummy { get; set; }
        public DatosObligatoriosEntidadDummyRule(EntidadDummy entidadDummy)
        {
            _entidadDummy = entidadDummy;
        }
        public override bool IsBroken()
        {
            if (string.IsNullOrEmpty(_entidadDummy.Name))
            {
                AddErrorMessage($"El Nombre de {nameof(EntidadDummy)} es obligatorio.");
            }

            return HasErrorMessages();
        }
    }
}
