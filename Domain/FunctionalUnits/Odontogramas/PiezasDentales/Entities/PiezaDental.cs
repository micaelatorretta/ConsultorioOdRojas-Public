using Domain.FunctionalUnits.PiezasDentales.Rules;
using Portable.Enums;
using Shared.Domain.Base;
using Shared.Domain.Extensions;

namespace Domain.FunctionalUnits.Odontogramas.Entities
{
    public partial class PiezaDental : BaseAggregateEntity
    {
        public PiezaDental() { }
        public byte NumeroPieza { get; set; }
        public TipoCuadrante Cuadrante { get; set; }
        public bool DenticionPermanente { get; set; }
        public List<CaraDental> CarasDentales { get; set; } = new();

    }

    public partial class PiezaDental
    {
        public override async Task Validate()
        {
            Rules.AddRules(new()
            { 
                // Se manda la Entidad Turno (this) para validar.
                new DatosObligatoriosPiezaDentalRule(this),
            });

            // Verifica que se cumplan las reglas y si hay un fallo lanza
            // una excepcion del tipo BusinessRuleException.
            Rules.CheckRules();
            await Task.CompletedTask;
        }
    }
}