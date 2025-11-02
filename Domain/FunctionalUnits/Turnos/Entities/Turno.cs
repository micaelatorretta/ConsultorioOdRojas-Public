using Domain.FunctionalUnits.Pacientes.Entities;
using Domain.FunctionalUnits.Turnos.Rules;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Turnos.Entities
{
    public partial class Turno : BaseAggregateEntity
    {
        public Turno()
        {
                
        }
        public string Descripcion { get; set; } = string.Empty;
        public Paciente Paciente { get; set; } = null!;

        public DateOnly Fecha { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string PacienteName => $"{Paciente?.Nombre} {Paciente?.Apellido}";

        #region FKs
        public int PacienteId { get; set; }
        #endregion


    }

    public partial class Turno
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad Turno (this) para validar.
                new DatosObligatoriosTurnoRule(this),
                new ControlFechaTurnoRule(this),
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}
