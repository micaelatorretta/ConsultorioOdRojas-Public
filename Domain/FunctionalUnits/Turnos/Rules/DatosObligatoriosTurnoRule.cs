using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.Turnos.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.Turnos.Rules
{
    public class DatosObligatoriosTurnoRule : BaseBusinessRule
    {
        private Turno _turno { get; set; }
        public DatosObligatoriosTurnoRule(Turno turno)
        {
            _turno = turno;
        }
        public override bool IsBroken()
        {

            if (_turno.PacienteId == 0)
            {
                AddErrorMessage($"El paciente es obligatorio.");
            }

            if (string.IsNullOrEmpty(_turno.Descripcion))
            {
                AddErrorMessage($"La {nameof(Turno.Descripcion)} del {nameof(Turno)} es obligatoria.");
            }      
        

            return HasErrorMessages();
        }
    }
}
