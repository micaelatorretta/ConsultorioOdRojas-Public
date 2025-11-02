using Portable.Enums.Auth;
using Shared.Domain.Base;
using Domain.FunctionalUnits.Pacientes.Rules;
using Shared.Domain.Extensions;
using Domain.FunctionalUnits.Odontogramas.Entities;
using Domain.FunctionalUnits.HistoriasClinicas.Entities;
using Domain.FunctionalUnits.ObrasSociales.Entities;

namespace Domain.FunctionalUnits.Pacientes.Entities
{
    public partial class Paciente : BaseAggregateEntity
    {
        public Paciente() { }
        public HistoriaClinica? HistoriaClinica { get; set; } 
        public ObraSocial? ObraSocial { get; set; } 
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string DNI { get; set; } = string.Empty;
        public List<Odontograma> Odontogramas { get; set; } = new();

        #region FKs
        public int? ObraSocialId { get; set; }
        #endregion
    }
    public partial class Paciente
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad Paciente (this) para validar.
                new DatosObligatoriosPacienteRule(this)
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}
