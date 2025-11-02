using Domain.FunctionalUnits.EntidadDummies.Rules;
using Domain.ValueObjects;
using Shared.Domain.Base;
using Shared.Domain.Base.Interfaces;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.EntidadDummies.Entities
{
    /// <summary>
    /// Clase Aggregate
    /// </summary>
    public partial class EntidadDummyD : BaseAggregateEntity
    {
        public EntidadDummyD() { }

        public string Name { get; set; } = string.Empty;

        // Relacion N -> N con aggregate
        public List<EntidadDummy> DummiesA { get; set; } = new();
    }

    public partial class EntidadDummyD
    {

        public override async Task Validate()
        {
            Rules.AddRules(new() 
            { 
                // Se manda la EntidadDummyD (this) para validar.
                //new DatosObligatoriosEntidadDummyRule(this)
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}
