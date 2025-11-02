using Domain.FunctionalUnits.ObrasSociales.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.ObrasSociales.Rules
{
    public class DatosObligatoriosObraSocialRule : BaseBusinessRule
    {
        private ObraSocial _obraSocial { get; set; }
        public DatosObligatoriosObraSocialRule(ObraSocial obraSocial)
        {
            _obraSocial = obraSocial;
        }
        public override bool IsBroken()
        {
            if (string.IsNullOrEmpty(_obraSocial.Nombre))
            {
                AddErrorMessage($"El {nameof(ObraSocial.Nombre)} de la {nameof(ObraSocial)} es obligatorio.");
            }

            if (string.IsNullOrEmpty(_obraSocial.Codigo))
            {
                AddErrorMessage($"El {nameof(ObraSocial.Codigo)} de la {nameof(ObraSocial)} es obligatorio.");
            }

            return HasErrorMessages();
        }
    }
}
