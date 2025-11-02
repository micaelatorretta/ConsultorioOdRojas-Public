using Domain.FunctionalUnits.EntidadDummies.Entities;
using Domain.FunctionalUnits.Turnos.Entities;
using Shared.Domain.Base;

namespace Domain.FunctionalUnits.Turnos.Rules
{
    public class ControlFechaTurnoRule : BaseBusinessRule
    {
        private Turno _turno { get; set; }
        public ControlFechaTurnoRule(Turno turno)
        {
            _turno = turno;
        }
        public override bool IsBroken()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            if (_turno.Fecha < today)
            {
                AddErrorMessage($"La {nameof(Turno.Fecha)} del {nameof(Turno)} es incorrecta.");
            }

            if(_turno.HoraInicio > _turno.HoraFin)
            {
                AddErrorMessage($"La hora de inicio debe ser menor a la hora de fin.");
            }
            return HasErrorMessages();
        }
    }
}
