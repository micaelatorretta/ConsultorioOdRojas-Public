using Domain.FunctionalUnits.CarasDentales.Rules;
using Portable.Enums;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Odontogramas.Entities
{
    public partial class CaraDental : BaseAggregateEntity
    {
        public CaraDental() { }
        public TipoCara CaraDentaria { get; set; }
        public PiezaDental PiezaDental { get; set; } = null!;
    }

    public partial class CaraDental
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad Turno (this) para validar.
                new DatosObligatoriosCaraDentalRule(this),
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}