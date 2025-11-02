using Domain.FunctionalUnits.ObrasSociales.Rules;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.ObrasSociales.Entities
{
    public partial class ObraSocial : BaseAggregateEntity
    {
        public ObraSocial() { }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;

    }

    public partial class ObraSocial
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad Turno (this) para validar.
                new DatosObligatoriosObraSocialRule(this),
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}