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
    public partial class EntidadDummy : BaseAggregateEntity
    {
        public EntidadDummy() { }

        public string Name { get; set; } = string.Empty;
        // Relacion 1 a 1
        public EntidadDummyC DummyC { get; set; } = null!;

        // ValueObject
        public ContactoTelefonico? Contacto { get; set; }

        // Relacion 1 -> N sin aggregate
        public List<EntidadDummyB> DummiesB { get; set; } = new();

        // Relacion N -> N con aggregate
        public List<EntidadDummyD> DummiesD { get; set; } = new();
    }

    public partial class EntidadDummy
    {

        public override async Task Validate()
        {
            Rules.AddRules(new() 
            { 
                // Se manda la EntidadDummy (this) para validar.
                new DatosObligatoriosEntidadDummyRule(this)
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}
