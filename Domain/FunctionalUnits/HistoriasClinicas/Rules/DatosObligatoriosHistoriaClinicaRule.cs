using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.HistoriasClinicas.Rules
{
    public class DatosObligatoriosHistoriaClinicaRule : BaseBusinessRule
    {
        private HistoriaClinica _HistoriaClinica { get; set; }
        public DatosObligatoriosHistoriaClinicaRule(HistoriaClinica historiaClinica)
        {
            _HistoriaClinica = historiaClinica;
        }
        public override bool IsBroken()
        {
            //if (string.IsNullOrEmpty(_HistoriaClinica.Nombre))
            //{
            //    AddErrorMessage($"El {nameof(HistoriaClinica.Nombre)} del {nameof(HistoriaClinica)} es obligatorio.");
            //}
            //if (string.IsNullOrEmpty(_HistoriaClinica.Apellido))
            //{
            //    AddErrorMessage($"El {nameof(HistoriaClinica.Apellido)} del {nameof(HistoriaClinica)} es obligatorio.");
            //}

            return HasErrorMessages();
        }


    }
}
