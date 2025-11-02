using Domain.FunctionalUnits.Prestaciones.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.Prestaciones.Rules
{
    public class DatosObligatoriosNomencladorRule : BaseBusinessRule
    {
        private Nomenclador _nomenclador { get; set; }
        public DatosObligatoriosNomencladorRule(Nomenclador nomenclador)
        {
            _nomenclador = nomenclador;
        }
        public override bool IsBroken()
        {

            // Validación de Nomenclador
            if (string.IsNullOrEmpty(_nomenclador.CodigoNacional))
            {
                AddErrorMessage($"El {nameof(Nomenclador.CodigoNacional)} del {nameof(Nomenclador)} es obligatorio y debe ser válido.");
            }  
            
            if (string.IsNullOrEmpty(_nomenclador.Descripcion))
            {
                AddErrorMessage($"La {nameof(Nomenclador.Descripcion)} del {nameof(Nomenclador)} es obligatorio y debe ser válido.");
            }
           
            if (_nomenclador.Importe <= 0)
            {
                AddErrorMessage($"El {nameof(Nomenclador.Importe)} del {nameof(Nomenclador)} es obligatorio y debe ser mayor a 0.");
            }

            return HasErrorMessages();
        }
    }
}
